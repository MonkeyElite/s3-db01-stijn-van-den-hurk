import RequestList from './RequestList';
import RequestForm from './RequestForm';
import RequestItem from './RequestItem';

function Request() {
  return (
    <div>
        <h1>Main Page</h1>
        <RequestList />
        <RequestForm />
        <RequestItem request={{ id: 1 }} />
    </div>
);
}

export default Request;
