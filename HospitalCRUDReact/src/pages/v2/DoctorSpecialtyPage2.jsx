import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useModal } from "../../hooks/useModal";
import { usePagination } from "../../hooks/usePagination";
import {
  createDoctorSpecialty,
  deleteDoctorSpecialty,
  getDoctorSpecialties,
  updateDoctorSpecialty,
} from "../../api/doctorSpecialtyApi";
import { useSearch } from "../../hooks/useSearch";
import { useEffect } from "react";
import PageHeader from "../../components/PageHeader";
import FilterSearch from "../../components/FilterSearch";
import Table from "../../components/Table";
import Pagination from "../../components/Pagination";
import Modal from "../../components/Modal";
import { toast } from "react-toastify";

const title = "Doctor Specialty";
const columns = [{ key: "name", label: "Name" }];
const modalFields = [
  {
    key: "name",
    label: "Name",
    type: "text",
    required: true,
  },
];
const DoctorSpecialtyPage2 = () => {
  const queryClient = useQueryClient();
  const { page, pageSize, setPage, setPageSize } = usePagination();
  const { modal, openCreate, openEdit, closeModal } = useModal();
  const { searchInput, search, handleChange, handleSearch } = useSearch();

  const { data, isLoading, error } = useQuery({
    queryKey: ["doctorSpecialties", page, pageSize, search],
    queryFn: () =>
      getDoctorSpecialties({
        page,
        pageSize,
        search,
      }),
  });
  const deleteMutation = useMutation({
    mutationFn: deleteDoctorSpecialty,

    onSuccess: () => {
      toast.success("Successfully deleted!");

      queryClient.invalidateQueries({
        queryKey: ["doctorSpecialties"],
      });
    },

    onError: (error) => {
      console.error(error);
    },
  });
  const createMutation = useMutation({
    mutationFn: createDoctorSpecialty,

    onSuccess: (_, doctorSpecialty) => {
      toast.success(`"${doctorSpecialty.name}" successfully created!`);

      closeModal();

      queryClient.invalidateQueries({
        queryKey: ["doctorSpecialties"],
      });
    },

    onError: (error) => {
      console.error(error);
    },
  });
  const updateMutation = useMutation({
    mutationFn: ({ id, data }) => updateDoctorSpecialty(id, data),

    onSuccess: (_, variables) => {
      toast.success(`"${variables.data.name}" successfully updated!`);

      closeModal();

      queryClient.invalidateQueries({
        queryKey: ["doctorSpecialties"],
      });
    },

    onError: (error) => {
      console.error(error);
    },
  });
  useEffect(() => {
    if (data && search) {
      if (data.totalCount > 0) {
        toast.success(
          `Found ${data.totalCount} doctor ${
            data.totalCount === 1 ? "specialty" : "specialties"
          }`,
        );
      }
    }
  }, [data, search]);

  //filtersearch

  const handleSearchFilterSearch = async () => {
    setPage(1);
    handleSearch();
  };
  //table
  const handleDelete = (id) => {
    deleteMutation.mutate(id);
  };
  //pagination

  //modal
  const handleSubmitModal = (doctorSpecialty) => {
    if (modal.selectedItem == null) {
      createMutation.mutate(doctorSpecialty);
    } else {
      updateMutation.mutate({
        id: doctorSpecialty.id,
        data: doctorSpecialty,
      });
    }
  };
  return (
    <>
      <PageHeader title={title} handleCreate={openCreate} />
      <FilterSearch
        handleChange={handleChange}
        handleSearchClick={handleSearchFilterSearch}
        name={searchInput}
      />
      <Table
        columns={columns}
        data={data?.items ?? []}
        handleEdit={openEdit}
        handleDelete={handleDelete}
        rowKey={"id"}
      />
      <Pagination
        // pagination={state.pagination}
        // loadData={loadDoctorSpecialties}
        pagination={{
          page,
          pageSize,
          totalCount: data?.totalCount ?? 0,
          totalPages: data?.totalPages ?? 0,
        }}
        onPageChange={setPage}
      />
      {/* {state.modal.show && (
        <Modal
          modalFields={modalFields}
          selectedItem={state.modal.selectedItem}
          onSubmit={handleSubmitModal}
          onClose={onClose}
          title={title}
        />
      )} */}
      {modal.show && (
        <Modal
          modalFields={modalFields}
          selectedItem={modal.selectedItem}
          onSubmit={handleSubmitModal}
          onClose={closeModal}
          title={title}
        />
      )}
    </>
  );
};

export default DoctorSpecialtyPage2;
