// import { useState } from "react";
// import { createContext } from "react";

// const ThemeContext = createContext();
// const ThemeProvider = ({ children }) => {
//   const [theme, setTheme] = useState("light");
//   document.documentElement.classList.add(theme);
//   const toggleTheme = () => {
//     setTheme((prev) => {
//       const newTheme = prev == "dark" ? "light" : "dark";
//       document.documentElement.classList.remove("light", "dark");
//       document.documentElement.classList.add(newTheme);

//       return newTheme;
//     });
//   };
//   return (
//     <>
//       <ThemeContext.Provider value={{ theme, toggleTheme }}>
//         {children}
//       </ThemeContext.Provider>
//     </>
//   );
// };

// export default ThemeProvider;
import { createContext, useEffect, useState } from "react";

export const ThemeContext = createContext();

const ThemeProvider = ({ children }) => {
  const [theme, setTheme] = useState("light");

  useEffect(() => {
    document.documentElement.classList.remove("light", "dark");
    document.documentElement.classList.add(theme);
  }, [theme]);

  const toggleTheme = () => {
    setTheme((prev) => (prev === "dark" ? "light" : "dark"));
  };

  return (
    <ThemeContext.Provider value={{ theme, toggleTheme }}>
      {children}
    </ThemeContext.Provider>
  );
};

export default ThemeProvider;
