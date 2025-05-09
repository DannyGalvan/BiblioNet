import { Button } from "@heroui/button";
import { Image } from "@heroui/image";
import { Navbar, NavbarBrand, NavbarContent, NavbarItem } from "@heroui/navbar";
import { Images } from "../../assets/images/images";
import { useAuth } from "../../hooks/useAuth";
import { Icon } from "../icons/Icon";

export interface NavbarAppProps {
  toggleDrawer: () => void;
}

export const NavbarApp = ({ toggleDrawer }: NavbarAppProps) => {
  const { logout } = useAuth();
  return (
    <Navbar>
      <NavbarBrand>
        <NavbarItem>
          <Button
            color="primary"
            isIconOnly
            variant="flat"
            onPress={toggleDrawer}
          >
            <Icon name="bi bi-list" color="black" />
          </Button>
        </NavbarItem>
        <Image
          isBlurred
          isZoomed
          alt="BiblioNet"
          className=""
          src={Images.logo}
          width={36}
        />
        <p className="font-bold text-inherit">BiblioNet</p>
      </NavbarBrand>
      <NavbarContent
        className="hidden sm:flex gap-4"
        justify="center"
      ></NavbarContent>
      <NavbarContent justify="end">
        <NavbarItem>
          <Button onPress={logout} color="danger">
            Logout
            <Icon name="bi bi-box-arrow-right" color="white" />
          </Button>
        </NavbarItem>
      </NavbarContent>
    </Navbar>
  );
};
