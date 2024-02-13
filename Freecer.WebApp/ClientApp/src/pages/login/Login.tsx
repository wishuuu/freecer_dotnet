import {Input} from "@/components/ui/input.tsx";
import {useEffect, useState} from "react";
import {useAuth} from "@/common/hooks/useAuth.ts";
import {Button} from "@/components/ui/button.tsx";
import {useUser} from "@/common/hooks/useUser.ts";


const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const { login, logout } = useAuth();
    const { user } = useUser();
    
    useEffect(() => {
        
    });
    
    return (
        <div>
            <h1>Login</h1>
            <Input placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} />
            <Input placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
            <Button onClick={() => login(username, password)}>Login</Button>
            <Button onClick={() => logout()}>Logout</Button>
            <h2>User</h2>
            <pre>{JSON.stringify(user, null, 2)}</pre>
        </div>
    )
}

export default Login