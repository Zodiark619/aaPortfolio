import { getDoctorSpecialtyDropdown } from "../api/doctorSpecialtyApi";
import { toast } from "react-toastify";
import { initialState, reducer } from "../reducer/reducer";
import { useEffect, useReducer, useState } from "react";
import {
  createDoctor,
  deleteDoctor,
  getDoctors,
  updateDoctor,
} from "../api/doctorApi";
import PageHeader from "../components/PageHeader";
import FilterSearch from "../components/FilterSearch";
import Table from "../components/Table";
import Pagination from "../components/Pagination";
import Modal from "../components/Modal";

const columns = [
  { key: "firstName", label: "First Name" },
  { key: "lastName", label: "Last Name" },
  { key: "specialtyName", label: "Doctor Specialty" },
];
const title = "Doctor";

const DoctorPage = () => {
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
    {
      key: "specialtyId",
      label: "Doctor Specialty",
      type: "select",
      required: true,
      options: state.doctorSpecialties,
      optionValue: "id",
      optionLabel: "name",
    },
  ];

  const loadDoctorSpecialties = async () => {
    try {
      const data = await getDoctorSpecialtyDropdown();

      dispatch({
        type: "LOAD_SPECIALTIES_SUCCESS",
        payload: data,
      });
    } catch (error) {
      console.error(error);
    }
  };
  useEffect(() => {
    loadDoctors(1);
    loadDoctorSpecialties();
  }, []);
  const loadDoctors = async (pageNumber) => {
    dispatch({
      type: "LOAD_START",
    });

    try {
      const data = await getDoctors({
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
  //     toast.success(`Found ${state.totalCount} doctors`);
  //   }
  //   await loadDoctors(1);
  // };
  const handleSearchClick = async () => {
    const data = await loadDoctors(1);
    if (data.totalCount > 0) {
      console.log(data.totalCount);
      toast.success(
        `Found ${data.totalCount} doctor${data.totalCount === 1 ? "" : "s"}`,
      );
    }
  };
  const handleEdit = (doctor) => {
    dispatch({
      type: "OPEN_EDIT",
      payload: doctor,
    });
  };
  const handleDelete = async (id) => {
    try {
      await deleteDoctor(id);
      toast.success(`Successfully deleted!`);

      await loadDoctors(1);
    } catch (error) {
      console.error(error);
    }
  };
  const handleSubmitModal = async (doctor) => {
    try {
      if (state.modal.selectedItem == null) {
        await createDoctor(doctor);

        toast.success(
          `"${doctor.firstName} ${doctor.lastName}" successfully created!`,
        );
      } else {
        await updateDoctor(doctor.id, doctor);

        toast.success(
          `"${doctor.firstName} ${doctor.lastName}" successfully updated!`,
        );
      }

      dispatch({
        type: "CLOSE_MODAL",
      });

      await loadDoctors(1);
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
        rowKey={"id"}
      />
      <Pagination pagination={state.pagination} loadData={loadDoctors} />
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

export default DoctorPage;
