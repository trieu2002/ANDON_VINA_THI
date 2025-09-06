import { createBrowserRouter } from "react-router-dom";
import Login from "../pages/Login";
import AndonPage from "../pages/Andon";
import ProtectedRoute from "../config/ProtecdRoute";
import NotFound from "../pages/NotFound";
const AppRoutes=createBrowserRouter([
   {path:"/login", element:<Login/>},
    {
      path:"/",
      element:<ProtectedRoute/>,
      children:[
         {path:"/andon",element:<AndonPage/>}
      ]
    },
    {path:"*",element:<NotFound/>}
   
]);
export default AppRoutes;