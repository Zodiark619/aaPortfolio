import { useEffect, useState } from "react";
import { getAll } from "../api/task";
import TaskTable from "../table/TaskTable";
import { useAuth } from "../context/AuthContext";
import TaskPagination from "../table/TaskPagination";

const Task = () => {
  const { user } = useAuth();
  const [tasks, setTasks] = useState({ items: [] });
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(3);
  useEffect(() => {
    const fetchTasks = async () => {
      const data = await getAll(user.token, page, pageSize);
      setTasks(data);
    };

    fetchTasks();
  }, [user.token, page, pageSize]);
  return (
    <>
      <div>
        <div>
          <TaskTable data={tasks.items} />
          <TaskPagination
            currentPage={tasks.currentPage}
            totalPages={tasks.totalPages}
            totalItems={tasks.totalItems}
            setPage={setPage}
          />
        </div>
      </div>
    </>
  );
};

export default Task;
