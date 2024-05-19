import RequestList from '../components/RequestList';
import NavBar from '../components/navbar';

import { Outlet } from 'react-router-dom';

function RequestPage() {
  return (
    <div>
        <NavBar page="Request"/>
        <div class="flex justify-center">
          <div>
            <RequestList />
          </div>
          <Outlet />
        </div>
    </div>
  );
}

export default RequestPage;
