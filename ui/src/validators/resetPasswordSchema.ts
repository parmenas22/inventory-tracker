import { z } from "zod";

const passwordRules = z
  .string()
  .regex(
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&.#^()[\]{}<>])[A-Za-z\d@$!%*?&.#^()[\]{}<>]{6,}$/,
    "Password must include at least one uppercase letter, one lowercase letter, one number, and one special character"
  )
  .min(6, "Password must be at least 6 characters long");

export const resetPasswordSchema = z
  .object({
    newPassword: passwordRules,
    confirmPassword: z.string(),
  })
  .refine((data) => data.newPassword === data.confirmPassword, {
    message: "Passwords do not match",
    path: ["confirmPassword"],
  });

export type ResetPasswordSchema = z.infer<typeof resetPasswordSchema>;
