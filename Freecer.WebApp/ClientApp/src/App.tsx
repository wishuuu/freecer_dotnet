import './App.css'
import CreateRoutes from "./common/router/ReactRouter.tsx";
import {createBrowserRouter, RouterProvider} from "react-router-dom";

const router = createBrowserRouter(CreateRoutes());

function App() {
  return (
      <RouterProvider router={router}/>
  );
}

export default App;
