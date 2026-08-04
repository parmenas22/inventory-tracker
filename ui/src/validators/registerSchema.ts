import { z } from "zod";

const passwordValidation = z
  .string()
  .regex(
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&.#^()[\]{}<>])[A-Za-z\d@$!%*?&.#^()[\]{}<>]{6,}$/, 
    "Password must include at least one uppercase, one lowercase, one number, and one special character"
  )
  .min(6, "Password must be at least 6 characters long");

export const registerSchema = z
  .object({
    firstName: z.string().min(1, "First name is required"),
    lastName: z.string().min(1, "Last name is required"),
    email: z.string().email("Please input a valid email"),
    password: passwordValidation,
    confirmPassword: z.string(),
  })
  .refine((data) => data.password === data.confirmPassword, {
    path: ["confirmPassword"],
    message: "Passwords must match",
  });

export type RegisterSchema = z.infer<typeof registerSchema>;
