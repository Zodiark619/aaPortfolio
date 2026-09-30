import { getDoctorSpecialtyDropdown } from "../api/doctorSpecialtyApi";
import { toast } from "react-toastify";
import { initialState, reducer } from "../reducer/reducer";
import { useEffect, useReducer, useState } from "react";
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
    key: "doctorSpecialtyId",
    label: "Doctor Specialty",
    type: "select",
    required: true,
    options: state.doctorSpecialties,
  },
];

const columns = [
  { key: "firstName", label: "First Name" },
  { key: "lastName", label: "Last Name" },
  { key: "doctorSpecialtyId", label: "Doctor Specialty" },
];
const title = "Doctor";

const DoctorPage = () => {
  const [state, dispatch] = useReducer(reducer, initialState);
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
  return <></>;
};

export default DoctorPage;
