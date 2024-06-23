import React from "react";
import CreateSessionForm from "../../components/Sessions/CreateSessionForm";
import NavBar from "../../components/navbar";

function CreateSessionPage() {
  return (
    <div>
      <NavBar page="Session" />
      <div className="flex flex-col items-center mt-8">
        <h2 className="text-3xl text-white">Create Session</h2>
        <div>
          <CreateSessionForm />
        </div>
      </div>
    </div>
  );
}

export default CreateSessionPage;
