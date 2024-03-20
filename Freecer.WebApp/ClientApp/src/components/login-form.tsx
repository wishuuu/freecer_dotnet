import {Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle} from "@/components/ui/card.tsx";
import {Label} from "@/components/ui/label.tsx";
import {Input} from "@/components/ui/input.tsx";
import {Button} from "@/components/ui/button.tsx";
import {useState} from "react";
import {useAuth} from "@/common/hooks/useAuth.ts";
import {toast} from "sonner";



const LoginForm = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const {login} = useAuth();

    const handleLogin = () => {
        if (!username || !password || username.length < 3 || password.length < 3) {
            toast.error("Wprowadź poprawne dane logowania",{
                description: "Nazwa użytkownika i hasło muszą zawierać co najmniej 3 znaki",
                duration: 3000,
                action: {
                    label: "Zamknij",
                    onClick: () => {}
                }
            });
            return;
        }
        login(username, password).then(() => window.location.reload());
    };

    return (
        <Card className={"w-[350px] m-auto"}>
            <CardHeader>
                <CardTitle>
                    Login
                </CardTitle>
                <CardDescription>
                    Wprowadź swoje dane logowania
                </CardDescription>
            </CardHeader>
            <CardContent>
                <div className={"grid w-full items-center gap-4"}>
                    <div className={"flex flex-col space-y-1.5"}>
                        <Label>Nazwa użytkownika</Label>
                        <Input placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)}/>
                    </div>
                    <div className={"flex flex-col space-y-1.5"}>
                        <Label>Hasło</Label>
                        <Input placeholder="Password" type={"password"} value={password}
                               onChange={(e) => setPassword(e.target.value)}/>
                    </div>
                </div>
            </CardContent>
            <CardFooter className={"flex justify-between"}>
                <Button onClick={handleLogin}>Login</Button>
            </CardFooter>
        </Card>
    )
}

export default LoginForm