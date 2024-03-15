import logo from './logo.svg';
import RequestList from './pages/Request/RequestList';
import RequestForm from './pages/Request/RequestForm';
import './App.css';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
        <RequestList />
        <RequestForm />
      </header>
    </div>
  );
}

export default App;