import RequestList from "./components/RequestList";
import RequestForm from "./components/RequestForm";
import NavBar from "./components/navbar";

function App() {
  return (
    <div
      className="App"
      class="bg-gradient-to-bl from-slate-800 to-slate-700 h-screen"
    >
      <header>
        <NavBar />
      </header>
      <div class="flex justify-center mt-5">
        <RequestList />
      </div>
      <div class="flex justify-center">
        <RequestForm />
      </div>
    </div>
  );
}

export default App;
