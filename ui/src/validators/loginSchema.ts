import { z } from "zod";

export const loginSchema = z.object({
  email: z.string().email("Please input a valid email"),
  password: z
    .string()
    .regex(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&.#^()[\]{}<>])[A-Za-z\d@$!%*?&.#^()[\]{}<>]{6,}$/,
      "Password must include at least one uppercase, one lowercase, one number, and one special character"
    )
    .min(6, "Password must be at least 6 characters long"),
});

export type LoginSchema = z.infer<typeof loginSchema>;
