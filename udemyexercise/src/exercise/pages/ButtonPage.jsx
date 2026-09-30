import { useEffect, useState } from "react";
import Button from "../components/Button.tsx";
import "../../pages/home.css";
import { GoBell } from "react-icons/go";

const ButtonPage = () => {
  const [meong, setMeong] = useState("");
  return (
    <>
      <div>
        <Button primary onClick={() => console.log("asdsad")}>
          <GoBell />
          asd
        </Button>
        <Button success onMouseEnter={() => setMeong("aa")}>
          lohrt wkmerm2
        </Button>
        <Button danger onMouseLeave={() => setMeong("www")}>
          lohrt wkmerm2
        </Button>
        <Button warning>lohrt wkmerm2</Button>
        <Button secondary>lohrt wkmerm2</Button>
        <button
          onMouseEnter={() => setMeong("NATIVE ENTER")}
          onMouseLeave={() => setMeong("NATIVE LEAVE")}
        >
          Native button
        </button>
        {meong}
      </div>
    </>
  );
};

export default ButtonPage;
