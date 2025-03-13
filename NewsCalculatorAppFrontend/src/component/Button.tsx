import { ButtonHTMLAttributes } from "react";

type ButtonProps = ButtonHTMLAttributes<HTMLButtonElement> & {
  label: string;
  variant: "primary" | "secondary";
};

export default function Button({ label, variant, ...props }: ButtonProps) {
  return (
    <button
      className={`btn ${
        variant === "primary"
          ? "bg-[#7424DA] text-white"
          : "bg-[#FAF6FF] text-black"
      } w-fit`}
      {...props}
    >
      {label}
    </button>
  );
}
