import { useState } from "react";

export const useModal = () => {
  const [modal, setModal] = useState({
    show: false,
    selectedItem: null,
  });

  const openCreate = () => {
    setModal({
      show: true,
      selectedItem: null,
    });
  };

  const openEdit = (item) => {
    setModal({
      show: true,
      selectedItem: item,
    });
  };

  const closeModal = () => {
    setModal({
      show: false,
      selectedItem: null,
    });
  };

  return {
    modal,
    openCreate,
    openEdit,
    closeModal,
  };
};
