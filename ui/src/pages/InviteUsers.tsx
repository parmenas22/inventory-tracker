import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { invitationService, type InvitationRole } from "@/services/invitation.service";
import { invitationSchema, type InvitationSchema } from "@/validators/invitationSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { toast } from "sonner";

const InviteUsers = () => {
  const [roles, setRoles] = useState<InvitationRole[]>([]);
  const [loadingRoles, setLoadingRoles] = useState(true);
  const [sending, setSending] = useState(false);
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<InvitationSchema>({
    resolver: zodResolver(invitationSchema),
  });

  useEffect(() => {
    const loadRoles = async () => {
      try {
        const response = await invitationService.getRoles();
        if (!response.succeeded || !response.value) {
          toast.error(response.message ?? "Unable to load roles");
          return;
        }
        setRoles(response.value);
      } catch {
        toast.error("Unable to load roles");
      } finally {
        setLoadingRoles(false);
      }
    };

    void loadRoles();
  }, []);

  const onSubmit = async (data: InvitationSchema) => {
    setSending(true);
    try {
      const response = await invitationService.invite(data.email, data.roleId);
      if (response.succeeded) {
        toast.success(response.message);
        reset();
      } else {
        toast.error(response.message ?? "Unable to send invitation");
      }
    } catch (error) {
      console.error("Invitation request failed:", error);
      toast.error("Unable to send invitation");
    } finally {
      setSending(false);
    }
  };

  return (
    <div className="container mx-auto max-w-xl px-4 py-8">
      <Card>
        <CardHeader>
          <CardTitle>Invite a user</CardTitle>
          <CardDescription>
            Send an email invitation to new users. Invitations expire after 7 days.
          </CardDescription>
        </CardHeader>
        <CardContent>
          <form className="space-y-5" onSubmit={handleSubmit(onSubmit)}>
            <div className="space-y-2">
              <Label htmlFor="invite-email">Email</Label>
              <Input
                id="invite-email"
                type="email"
                autoComplete="email"
                placeholder="name@example.com"
                {...register("email")}
              />
              {errors.email && (
                <p className="text-sm text-red-600">{errors.email.message}</p>
              )}
            </div>
            <div className="space-y-2">
              <Label htmlFor="invite-role">Role</Label>
              <select
                id="invite-role"
                className="flex h-9 w-full rounded-md border border-input bg-transparent px-3 py-1 text-sm shadow-sm focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring"
                disabled={loadingRoles || roles.length === 0}
                defaultValue=""
                {...register("roleId")}
              >
                <option value="" disabled>
                  {loadingRoles ? "Loading roles..." : "Select a role"}
                </option>
                {roles.map((role) => (
                  <option key={role.roleId} value={role.roleId}>
                    {role.name}
                  </option>
                ))}
              </select>
              {errors.roleId && (
                <p className="text-sm text-red-600">{errors.roleId.message}</p>
              )}
            </div>
            <Button type="submit" disabled={sending || loadingRoles || roles.length === 0}>
              {sending ? "Sending..." : "Send invitation"}
            </Button>
          </form>
        </CardContent>
      </Card>
    </div>
  );
};

export default InviteUsers;
