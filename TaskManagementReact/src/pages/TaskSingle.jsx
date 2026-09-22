import { useParams } from "react-router";
import { getById } from "../api/task";
import { useEffect, useState } from "react";
import { useAuth } from "../context/AuthContext";

const TaskSingle = () => {
  const { user } = useAuth();

  const { id } = useParams();
  const [task, setTask] = useState(null);

  useEffect(() => {
    const fetchTask = async () => {
      const data = await getById(id);
      setTask(data);
    };

    fetchTask();
  }, [id]);

  if (!task) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      <h1>
        {task.id} - {task.name}
      </h1>
      <div>
        <p>Category = {TaskSingle.category}</p>
        <p>Status = {TaskSingle.taskStatus}</p>
        <p>Description = {TaskSingle.description}</p>
        <p>Created At = {TaskSingle.createdAt}</p>
        <p>Created By Name = {TaskSingle.createdByName}</p>

        <p>Due Date = {TaskSingle.dueDate}</p>

        {!task.isCompleted && task.taskStatus === "Available" && (
          <div>
            <button className="btn btn-primary">Submit</button>
          </div>
        )}
        {task.isCompleted && (
          <div>
            <p>Is Completed = {TaskSingle.isCompleted}</p>
            <p>Submitted By = {TaskSingle.submittedBy}</p>
            <p>Finished Date = {TaskSingle.finishedDate}</p>
          </div>
        )}
      </div>
    </div>
  );
};

export default TaskSingle;
