import { useState } from "react";

export const useSearch = () => {
  const [search, setSearch] = useState("");
  const [searchInput, setSearchInput] = useState("");
  const handleChange = (e) => {
    setSearchInput(e.target.value);
  };

  const handleSearch = () => {
    // optional: reset pagination here
    setSearch(searchInput);
  };

  return {
    searchInput,
    search,
    handleChange,
    handleSearch,
  };
};
