import { toast } from "react-toastify";
import { initialState, reducer } from "../reducer/reducer";
import { useEffect, useReducer, useState } from "react";
import {
  createAdmission,
  deleteAdmission,
  getAdmissions,
  updateAdmission,
} from "../api/AdmissionApi";
import PageHeader from "../components/PageHeader";
import FilterSearch from "../components/FilterSearch";
import Table from "../components/Table";
import Pagination from "../components/Pagination";
import Modal from "../components/Modal";
import { getDoctorDropdown } from "../api/doctorApi";
import { getPatientDropdown } from "../api/patientApi";

const columns = [
  { key: "patientName", label: "Patient Name" },
  { key: "attendingDoctorName", label: "Doctor Name" },
  { key: "diagnosis", label: "Diagnosis" },
  { key: "admissionDate", label: "Admission Date", type: "date" },
  { key: "dischargeDate", label: "Discharge Date", type: "date" },
];
const title = "Admission";

const AdmissionPage = () => {
  const [state, dispatch] = useReducer(reducer, initialState);
  const modalFields = [
    {
      key: "patientId",
      label: "Patient Name",
      type: "select",
      required: true,
      options: state.patients,
      optionValue: "id",
      optionLabel: "name",
    },
    {
      key: "attendingDoctorId",
      label: "Doctor Name",
      type: "select",
      required: true,
      options: state.doctors,
      optionValue: "id",
      optionLabel: "name",
    },
    {
      key: "diagnosis",
      label: "Diagnosis",
      type: "text",
      required: true,
    },
    {
      key: "admissionDate",
      label: "Admission Date",
      type: "date",
      required: true,
    },
    {
      key: "dischargeDate",
      label: "Discharge Date",
      type: "date",
      required: false,
    },
  ];

  const loadDoctors = async () => {
    try {
      const data = await getDoctorDropdown();

      dispatch({
        type: "LOAD_DOCTORS_SUCCESS",
        payload: data,
      });
    } catch (error) {
      console.error(error);
    }
  };
  const loadPatients = async () => {
    try {
      const data = await getPatientDropdown();

      dispatch({
        type: "LOAD_PATIENTS_SUCCESS",
        payload: data,
      });
    } catch (error) {
      console.error(error);
    }
  };
  useEffect(() => {
    loadAdmissions(1);
    loadDoctors();
    loadPatients();
  }, []);
  const loadAdmissions = async (pageNumber) => {
    dispatch({
      type: "LOAD_START",
    });

    try {
      const data = await getAdmissions({
        page: pageNumber,
        pageSize: state.pagination.pageSize,
        search: state.search.trim(),
      });

      dispatch({
        type: "LOAD_SUCCESS",
        payload: data,
      });
      return data;
    } catch (error) {
      dispatch({
        type: "LOAD_ERROR",
        payload: error.message,
      });
      throw error;
    }
  };
  const handleCreate = () => {
    dispatch({
      type: "OPEN_CREATE",
    });
  };

  const handleChange = (e) => {
    dispatch({
      type: "SET_SEARCH",
      payload: e.target.value,
    });
  };

  // const handleSearchClick = async () => {
  //   if (state.totalCount > 0) {
  //     toast.success(`Found ${state.totalCount} Admissions`);
  //   }
  //   await loadAdmissions(1);
  // };
  const handleSearchClick = async () => {
    const data = await loadAdmissions(1);
    if (data.totalCount > 0) {
      console.log(data.totalCount);
      toast.success(
        `Found ${data.totalCount} Admission${data.totalCount === 1 ? "" : "s"}`,
      );
    }
  };
  const handleEdit = (Admission) => {
    dispatch({
      type: "OPEN_EDIT",
      payload: Admission,
    });
  };
  const handleDelete = async (id) => {
    try {
      await deleteAdmission(id);
      toast.success(`Successfully deleted!`);

      await loadAdmissions(1);
    } catch (error) {
      console.error(error);
    }
  };
  const handleSubmitModal = async (Admission) => {
    try {
      if (state.modal.selectedItem == null) {
        await createAdmission(Admission);

        toast.success(`Admission successfully created!`);
      } else {
        await updateAdmission(Admission.admissionId, Admission);

        toast.success(`Admission successfully updated!`);
      }

      dispatch({
        type: "CLOSE_MODAL",
      });

      await loadAdmissions(1);
    } catch (error) {
      console.error(error);
    }
  };
  const onClose = () => {
    dispatch({
      type: "CLOSE_MODAL",
    });
  };
  return (
    <>
      <PageHeader title={title} handleCreate={handleCreate} />
      <FilterSearch
        handleChange={handleChange}
        handleSearchClick={handleSearchClick}
        name={state.search}
        // name={name}
      />
      <Table
        columns={columns}
        data={state.data}
        handleEdit={handleEdit}
        handleDelete={handleDelete}
        rowKey={"admissionId"}
      />
      <Pagination pagination={state.pagination} loadData={loadAdmissions} />
      {state.modal.show && (
        <Modal
          modalFields={modalFields}
          selectedItem={state.modal.selectedItem}
          onSubmit={handleSubmitModal}
          onClose={onClose}
          title={title}
        />
      )}
    </>
  );
};

export default AdmissionPage;
