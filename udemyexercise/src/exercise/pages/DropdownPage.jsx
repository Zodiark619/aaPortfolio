import { useState } from "react";
import Dropdown from "../components/Dropdown";

const DropdownPage = () => {
  const options = [
    { label: "label1", value: "label1value" },
    { label: "label2", value: "label2value" },
    { label: "label3", value: "label3value" },
  ];
  const [selection, setSelection] = useState(null);
  const handleSelect = (input) => {
    setSelection(input);
  };
  return (
    <>
      <div>
        <Dropdown
          options={options}
          selection={selection}
          handleSelect={handleSelect}
        />
      </div>
    </>
  );
};

export default DropdownPage;
