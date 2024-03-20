import {useUser} from "@/common/hooks/useUser.ts";
import {Navigate} from "react-router-dom";

interface ProtectedRouteProps extends React.HTMLAttributes<HTMLDivElement> {
    userRights: string[];
}

export default function ProtectedRoute(props: ProtectedRouteProps) {
    const {user} = useUser();
    
    if (user) {
        return (
            <>
                {
                    props.children
                }
            </>
        )
    } else {
        return <Navigate to={"/login"}/>
    }
}