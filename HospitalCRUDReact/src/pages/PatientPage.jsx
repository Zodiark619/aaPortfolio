import { toast } from "react-toastify";
import { initialState, reducer } from "../reducer/reducer";
import { useEffect, useReducer, useState } from "react";
import {
  createPatient,
  deletePatient,
  getPatients,
  updatePatient,
} from "../api/patientApi";
import PageHeader from "../components/PageHeader";
import FilterSearch from "../components/FilterSearch";
import Table from "../components/Table";
import Pagination from "../components/Pagination";
import Modal from "../components/Modal";
import { getProvinceDropdown } from "../api/provinceApi";

const columns = [
  { key: "firstName", label: "First Name" },
  { key: "lastName", label: "Last Name" },
  { key: "gender", label: "Gender" },
  { key: "dateOfBirth", label: "Date of Birth" },
  { key: "city", label: "City" },
  { key: "provinceName", label: "Province Name" },
  { key: "allergies", label: "Allergies" },
  { key: "height", label: "Height" },
  { key: "weight", label: "Weight" },
];
const title = "Patient";

const PatientPage = () => {
  const [state, dispatch] = useReducer(reducer, initialState);
  const modalFields = [
    {
      key: "firstName",
      label: "First Name",
      type: "text",
      required: true,
    },
    {
      key: "lastName",
      label: "Last Name",
      type: "text",
      required: true,
    },
    //
    {
      key: "gender",
      label: "Gender",
      type: "text",
      required: true,
    },
    {
      key: "dateOfBirth",
      label: "Date of Birth",
      type: "date",
      required: true,
    },
    {
      key: "city",
      label: "City",
      type: "text",
      required: true,
    },
    //
    {
      key: "provinceId",
      label: "Province Name",
      type: "select",
      required: true,
      options: state.provinces,
      optionValue: "provinceId",
      optionLabel: "provinceName",
    },
    {
      key: "allergies",
      label: "Allergies",
      type: "text",
      required: true,
    },
    {
      key: "height",
      label: "Height",
      type: "number",
      required: true,
    },
    {
      key: "weight",
      label: "Weight",
      type: "number",
      required: true,
    },
  ];

  const loadProvinces = async () => {
    try {
      const data = await getProvinceDropdown();

      dispatch({
        type: "LOAD_PROVINCES_SUCCESS",
        payload: data,
      });
    } catch (error) {
      console.error(error);
    }
  };
  useEffect(() => {
    loadPatients(1);
    loadProvinces();
  }, []);
  const loadPatients = async (pageNumber) => {
    dispatch({
      type: "LOAD_START",
    });

    try {
      const data = await getPatients({
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
  //     toast.success(`Found ${state.totalCount} Patients`);
  //   }
  //   await loadPatients(1);
  // };
  const handleSearchClick = async () => {
    const data = await loadPatients(1);
    if (data.totalCount > 0) {
      console.log(data.totalCount);
      toast.success(
        `Found ${data.totalCount} Patient${data.totalCount === 1 ? "" : "s"}`,
      );
    }
  };
  const handleEdit = (Patient) => {
    dispatch({
      type: "OPEN_EDIT",
      payload: Patient,
    });
  };
  const handleDelete = async (id) => {
    try {
      await deletePatient(id);
      toast.success(`Successfully deleted!`);

      await loadPatients(1);
    } catch (error) {
      console.error(error);
    }
  };
  const handleSubmitModal = async (Patient) => {
    try {
      if (state.modal.selectedItem == null) {
        await createPatient(Patient);

        toast.success(
          `"${Patient.firstName} ${Patient.lastName}" successfully created!`,
        );
      } else {
        await updatePatient(Patient.patientId, Patient);

        toast.success(
          `"${Patient.firstName} ${Patient.lastName}" successfully updated!`,
        );
      }

      dispatch({
        type: "CLOSE_MODAL",
      });

      await loadPatients(1);
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
        rowKey={"patientId"}
      />
      <Pagination pagination={state.pagination} loadData={loadPatients} />
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

export default PatientPage;
