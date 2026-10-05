import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";

export const useCrud = (key, api) => {
  const queryClient = useQueryClient();

  // GET
  const getAll = useQuery({
    queryKey: [key],
    queryFn: api.getAll,
  });

  // POST
  const create = useMutation({
    mutationFn: api.create,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [key],
      });
    },
  });

  // PUT
  const update = useMutation({
    mutationFn: api.update,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [key],
      });
    },
  });

  // DELETE
  const remove = useMutation({
    mutationFn: api.remove,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [key],
      });
    },
  });

  return {
    getAll,
    create,
    update,
    remove,
  };
};
