import { useEffect, useReducer, useState } from "react";
import PageHeader from "../components/PageHeader";
import FilterSearch from "../components/FilterSearch";
import Table from "../components/Table";
import {
  createProvince,
  deleteProvince,
  getProvinces,
  updateProvince,
} from "../api/provinceApi";
import Pagination from "../components/Pagination";
import Modal from "../components/Modal";
import { toast } from "react-toastify";
import { initialState, reducer } from "../reducer/reducer";

////////////////// constants
const modalFields = [
  {
    key: "provinceName",
    label: "Province Name",
    type: "text",
    required: true,
  },
];

const columns = [
  { key: "provinceId", label: "Province Id" },
  { key: "provinceName", label: "Province Name" },
];
const title = "Province";
//

const ProvincePage = () => {
  const [state, dispatch] = useReducer(reducer, initialState);

  useEffect(() => {
    loadProvinces(1);
  }, []);
  const loadProvinces = async (pageNumber) => {
    dispatch({
      type: "LOAD_START",
    });

    try {
      const data = await getProvinces({
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
    const data = await loadProvinces(1);
    if (data.totalCount > 0) {
      console.log(data.totalCount);
      toast.success(
        `Found ${data.totalCount} ${data.totalCount === 1 ? "province" : "provinces"}`,
      );
    }
  };
  /////table
  const handleEdit = (Province) => {
    dispatch({
      type: "OPEN_EDIT",
      payload: Province,
    });
  };
  const handleDelete = async (id) => {
    try {
      await deleteProvince(id);
      toast.success(`Successfully deleted!`);

      await loadProvinces(1);
    } catch (error) {
      console.error(error);
    }
  };
  //////modal
  const handleSubmitModal = async (Province) => {
    try {
      if (state.modal.selectedItem == null) {
        await createProvince(Province);

        toast.success(`"${Province.provinceName}" successfully created!`);
      } else {
        await updateProvince(Province.provinceId, Province);

        toast.success(`"${Province.provinceName}" successfully updated!`);
      }

      dispatch({
        type: "CLOSE_MODAL",
      });

      await loadProvinces(1);
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
        rowKey={"provinceId"}
      />
      <Pagination pagination={state.pagination} loadData={loadProvinces} />
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

export default ProvincePage;
