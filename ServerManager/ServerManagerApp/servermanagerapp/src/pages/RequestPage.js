import RequestList from "../components/Request/RequestList";
import NavBar from "../components/navbar";

import { Link } from "react-router-dom";

function RequestPage() {
  return (
    <div>
      <NavBar page="Request" />
      <div class="flex justify-center">
        <div>
          <RequestList />
        </div>
      </div>
      <div class="flex justify-center">
        <Link
          to="/request/create"
          class="bg-blue-500 text-white border-none px-5 py-2.5 rounded cursor-pointer ml-2.5 mb-5"
          data-testid="create-request-button"
        >
          Create new request
        </Link>
      </div>
    </div>
  );
}

export default RequestPage;
