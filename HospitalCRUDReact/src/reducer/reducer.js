const initialState = {
  data: [],
  search: "",
  loading: false,
  error: null,
  doctorSpecialties: [],
  provinces: [],
  patients: [],
  doctors: [],

  pagination: {
    page: 1,
    pageSize: 5,
    totalCount: 0,
    totalPages: 0,
  },

  modal: {
    show: false,
    selectedItem: null,
  },
};

const reducer = (state, action) => {
  switch (action.type) {
    case "LOAD_DOCTORS_SUCCESS":
      return {
        ...state,
        doctors: action.payload,
      };
    case "LOAD_PATIENTS_SUCCESS":
      return {
        ...state,
        patients: action.payload,
      };
    case "LOAD_SPECIALTIES_SUCCESS":
      return {
        ...state,
        doctorSpecialties: action.payload,
      };
    case "LOAD_PROVINCES_SUCCESS":
      return {
        ...state,
        provinces: action.payload,
      };
    case "SET_SEARCH":
      return {
        ...state,
        search: action.payload,
      };

    case "LOAD_START":
      return {
        ...state,
        loading: true,
        error: null,
      };

    case "LOAD_SUCCESS":
      return {
        ...state,
        loading: false,

        data: action.payload.items,

        pagination: {
          page: action.payload.page,
          pageSize: action.payload.pageSize,
          totalCount: action.payload.totalCount,
          totalPages: action.payload.totalPages,
        },
      };

    case "LOAD_ERROR":
      return {
        ...state,
        loading: false,
        error: action.payload,
      };

    case "OPEN_CREATE":
      return {
        ...state,
        modal: {
          show: true,
          selectedItem: null,
        },
      };

    case "OPEN_EDIT":
      return {
        ...state,
        modal: {
          show: true,
          selectedItem: action.payload,
        },
      };

    case "CLOSE_MODAL":
      return {
        ...state,
        modal: {
          show: false,
          selectedItem: null,
        },
      };

    default:
      return state;
  }
};

export { initialState, reducer };
