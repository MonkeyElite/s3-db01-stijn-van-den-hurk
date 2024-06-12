import NavBar from '../components/navbar';
import ServerList from '../components/Server/ServerList';

import { Link } from "react-router-dom";

function SessionsPage() {
  return (
    <div>
        <NavBar page="Server"/>
        <div class="flex justify-center">
            <div>
            <ServerList />
            </div>
        </div>
        <div class="flex justify-center">
            <Link
            to="/server/create"
            class="bg-blue-500 text-white border-none px-5 py-2.5 rounded cursor-pointer ml-2.5 mb-5"
            data-testid="create-request-button"
            >
            Create new server
            </Link>
        </div>
    </div>
);
}

export default SessionsPage;
