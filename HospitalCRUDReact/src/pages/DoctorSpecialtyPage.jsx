import { useEffect, useReducer, useState } from "react";
import PageHeader from "../components/PageHeader";
import FilterSearch from "../components/FilterSearch";
import Table from "../components/Table";
import {
  createDoctorSpecialty,
  deleteDoctorSpecialty,
  getDoctorSpecialties,
  updateDoctorSpecialty,
} from "../api/doctorSpecialtyApi";
import Pagination from "../components/Pagination";
import Modal from "../components/Modal";
import { toast } from "react-toastify";
import { initialState, reducer } from "../reducer/reducer";
import { useQuery } from "@tanstack/react-query";
// const fetchDoctorSpecialties = async ({ queryKey }) => {
//   const {data,isLoading,isError,error} =  useQuery({

//   queryKey: ["doctorSpecialties"] ,
// queryFn:getDoctorSpecialties
// });
// if(isloading){
//   return "Loading...";
// }
// if(isError){
//   return <p>{error.message}</p>>
// }
//   return data;
// }
////////////////// constants
const modalFields = [
  {
    key: "name",
    label: "Name",
    type: "text",
    required: true,
  },
];

const columns = [{ key: "name", label: "Name" }];
const title = "Doctor Specialty";
//

const DoctorSpecialtyPage = () => {
  const doctorSpecialtyCrud = useCrud("doctorSpecialties", {
    createDoctorSpecialty,
    deleteDoctorSpecialty,
    getDoctorSpecialties,
    updateDoctorSpecialty,
  });

  const [state, dispatch] = useReducer(reducer, initialState);

  useEffect(() => {
    loadDoctorSpecialties(1);
  }, []);
  const loadDoctorSpecialties = async (pageNumber) => {
    dispatch({
      type: "LOAD_START",
    });

    try {
      const data = await getDoctorSpecialties({
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
  //pageheader
  const handleCreate = () => {
    dispatch({
      type: "OPEN_CREATE",
    });
  };

  //////////filtersearch

  const handleChange = (e) => {
    dispatch({
      type: "SET_SEARCH",
      payload: e.target.value,
    });
  };

  // if (state.search.trim().length > 1) {
  //   toast.success(`Searching "${state.search.trim()}"`);
  // }
  const handleSearchClick = async () => {
    const data = await loadDoctorSpecialties(1);
    if (data.totalCount > 0) {
      console.log(data.totalCount);
      toast.success(
        `Found ${data.totalCount} doctor ${data.totalCount === 1 ? "specialty" : "specialties"}`,
      );
    }
  };
  /////table
  const handleEdit = (doctorSpecialty) => {
    dispatch({
      type: "OPEN_EDIT",
      payload: doctorSpecialty,
    });
  };
  const handleDelete = async (id) => {
    try {
      await deleteDoctorSpecialty(id);
      toast.success(`Successfully deleted!`);

      await loadDoctorSpecialties(1);
    } catch (error) {
      console.error(error);
    }
  };
  //////modal
  const handleSubmitModal = async (doctorSpecialty) => {
    try {
      if (state.modal.selectedItem == null) {
        await createDoctorSpecialty(doctorSpecialty);

        toast.success(`"${doctorSpecialty.name}" successfully created!`);
      } else {
        await updateDoctorSpecialty(doctorSpecialty.id, doctorSpecialty);

        toast.success(`"${doctorSpecialty.name}" successfully updated!`);
      }

      dispatch({
        type: "CLOSE_MODAL",
      });

      await loadDoctorSpecialties(1);
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
      <Pagination
        pagination={state.pagination}
        loadData={loadDoctorSpecialties}
      />
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

export default DoctorSpecialtyPage;
