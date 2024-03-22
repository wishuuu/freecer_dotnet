"use client"

import {
    NavigationMenu, NavigationMenuItem, NavigationMenuList,
} from "@/components/ui/navigation-menu"
import NavigationItemsProvider from "@/components/navigation/navigation-items-provider.tsx";
import {ModeToggle} from "@/components/mode-toggle.tsx";

const MainNavigation = () => {

    return (
        <>
            <header
                className="flex h-20 w-full items-center">
                <div className="container flex h-20 px-4 items-center gap-2">
                    <p className={"flex items-center font-semibold"}>
                        <a href={"/"} className={"flex items-center"}>
                            Freecer
                        </a>
                    </p>
                    <nav className="ml-auto flex items-center space-x-4">
                        <NavigationMenu className={'min-w-[400px]'}>
                            <NavigationMenuList className={"space-x-1"}>
                                <NavigationItemsProvider/>
                                <NavigationMenuItem>
                                    <ModeToggle/>
                                </NavigationMenuItem>
                            </NavigationMenuList>
                        </NavigationMenu>
                    </nav>
                </div>
            </header>
        </>
    )
}

export default MainNavigation