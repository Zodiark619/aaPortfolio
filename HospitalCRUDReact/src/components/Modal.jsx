import { useEffect, useState } from "react";

const Modal = ({ modalFields, selectedItem, onSubmit, onClose, title }) => {
  const [formData, setFormData] = useState({});

  // Populate form when editing, empty form when creating
  useEffect(() => {
    setFormData(selectedItem ?? {});
  }, [selectedItem]);

  const handleChange = (e) => {
    const { name, value } = e.target;

    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = () => {
    onSubmit(formData);
  };

  return (
    <>
      <div className="modal show" tabIndex="-1" style={{ display: "block" }}>
        <div className="modal-dialog">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title">
                {selectedItem == null ? "Create" : "Update"} {title}
              </h5>

              <button
                type="button"
                className="btn-close"
                aria-label="Close"
                onClick={onClose}
              />
            </div>
            <form
              onSubmit={(e) => {
                e.preventDefault();
                handleSubmit(formData);
              }}
            >
              <div className="modal-body">
                {modalFields.map((field) => (
                  <div key={field.key} className="mb-3">
                    <label className="form-label">{field.label}</label>

                    {field.type === "select" ? (
                      <select
                        name={field.key}
                        value={formData[field.key] ?? ""}
                        onChange={handleChange}
                        className="form-select"
                        required={field.required}
                      >
                        <option value="">Select {field.label}</option>

                        {field.options?.map((option) => (
                          // <option key={option.id} value={option.id}>
                          //   {option.name}
                          // </option>
                          <option
                            key={option[field.optionValue]}
                            value={option[field.optionValue]}
                          >
                            {option[field.optionLabel]}
                          </option>
                        ))}
                      </select>
                    ) : (
                      <input
                        type={field.type}
                        name={field.key}
                        // value={formData[field.key] ?? ""}
                        value={
                          field.type === "date"
                            ? (formData[field.key]?.split("T")[0] ?? "")
                            : (formData[field.key] ?? "")
                        }
                        onChange={handleChange}
                        className="form-control"
                        required={field.required}
                      />
                    )}
                  </div>
                ))}
              </div>

              <div className="modal-footer">
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={onClose}
                >
                  Close
                </button>

                <button
                  // type="button"
                  // onClick={handleSubmit}
                  type="submit"
                  className="btn btn-primary"
                >
                  Save changes
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
      <div className="modal-backdrop show"></div>
    </>
  );
};

export default Modal;
