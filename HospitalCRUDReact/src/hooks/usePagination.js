import { useState } from "react";

export const usePagination = (initialPageSize = 5) => {
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(initialPageSize);

  return {
    page,
    pageSize,
    setPage,
    setPageSize,
  };
};
