import ServerForm from "../../components/Server/CreateServerForm";
import NavBar from "../../components/navbar";

function CreateServerPage() {
  return (
    <div>
      <NavBar page="Server" />
      <div class="flex flex-col items-center mt-8">
        <h2 class="text-3xl text-white">Create Server</h2>
        <div>
          <ServerForm />
        </div>
      </div>
    </div>
  );
}

export default CreateServerPage;
