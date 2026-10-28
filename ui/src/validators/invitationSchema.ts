import { z } from "zod";

export const invitationSchema = z.object({
  email: z.string().email("Please input a valid email"),
  roleId: z.string().min(1, "Please select a role"),
});

export const acceptInvitationSchema = z
  .object({
    firstName: z.string().trim().min(1, "First name is required").max(100),
    lastName: z.string().trim().min(1, "Last name is required").max(100),
    password: z
      .string()
      .regex(
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&.#^()[\]{}<>])[A-Za-z\d@$!%*?&.#^()[\]{}<>]{6,}$/,
        "Use at least 6 characters with uppercase, lowercase, a number, and a special character"
      ),
    confirmPassword: z.string(),
  })
  .refine((data) => data.password === data.confirmPassword, {
    path: ["confirmPassword"],
    message: "Passwords must match",
  });

export type InvitationSchema = z.infer<typeof invitationSchema>;
export type AcceptInvitationSchema = z.infer<typeof acceptInvitationSchema>;
