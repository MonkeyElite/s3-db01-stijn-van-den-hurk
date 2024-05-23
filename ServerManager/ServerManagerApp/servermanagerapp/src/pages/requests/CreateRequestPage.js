import RequestForm from "../../components/CreateRequestForm";
import NavBar from "../../components/navbar";

function CreateRequestPage() {
  return (
    <div>
      <NavBar page="Request" />
      <div class="flex flex-col items-center mt-8">
        <h2 class="text-3xl text-white">Create Request</h2>
        <div>
          <RequestForm />
        </div>
      </div>
    </div>
  );
}

export default CreateRequestPage;
