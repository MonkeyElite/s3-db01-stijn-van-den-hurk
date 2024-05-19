import NavBar from '../components/navbar';

function HomePage() {
  return (
    <div>
        <NavBar page="Home" />
        <div class="flex justify-center">
            <h1 class="text-3xl text-white">Main Page</h1>
        </div>
    </div>
);
}

export default HomePage;
