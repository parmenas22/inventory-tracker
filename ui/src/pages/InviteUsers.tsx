import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  invitationService,
  type InvitationRole,
  type UserAccessOverview,
} from "@/services/invitation.service";
import { invitationSchema, type InvitationSchema } from "@/validators/invitationSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { Clock3, Mail, Plus, Users } from "lucide-react";
import { useCallback, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { toast } from "sonner";

const emptyOverview: UserAccessOverview = {
  activeUsers: [],
  pendingInvitations: [],
};

const formatDate = (date: string) =>
  new Intl.DateTimeFormat(undefined, {
    dateStyle: "medium",
  }).format(new Date(date));

const InviteUsers = () => {
  const [overview, setOverview] = useState<UserAccessOverview>(emptyOverview);
  const [loadingOverview, setLoadingOverview] = useState(true);
  const [overviewError, setOverviewError] = useState<string | null>(null);
  const [roles, setRoles] = useState<InvitationRole[]>([]);
  const [loadingRoles, setLoadingRoles] = useState(true);
  const [inviteOpen, setInviteOpen] = useState(false);
  const [sending, setSending] = useState(false);
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<InvitationSchema>({
    resolver: zodResolver(invitationSchema),
    defaultValues: { email: "", roleId: "" },
  });

  const loadOverview = useCallback(async () => {
    setLoadingOverview(true);
    try {
      const response = await invitationService.getAdminOverview();
      if (!response.succeeded || !response.value) {
        const message = response.message ?? "Unable to load user access";
        setOverviewError(message);
        toast.error(message);
        return;
      }
      setOverview(response.value);
      setOverviewError(null);
    } catch (error) {
      console.error("User access request failed:", error);
      setOverviewError("Unable to load user access");
      toast.error("Unable to load user access");
    } finally {
      setLoadingOverview(false);
    }
  }, []);

  useEffect(() => {
    void loadOverview();

    const loadRoles = async () => {
      try {
        const response = await invitationService.getRoles();
        if (!response.succeeded || !response.value) {
          toast.error(response.message ?? "Unable to load roles");
          return;
        }
        setRoles(response.value);
      } catch (error) {
        console.error("Invitation roles request failed:", error);
        toast.error("Unable to load roles");
      } finally {
        setLoadingRoles(false);
      }
    };

    void loadRoles();
  }, [loadOverview]);

  const onSubmit = async (data: InvitationSchema) => {
    setSending(true);
    try {
      const response = await invitationService.invite(data.email, data.roleId);
      if (!response.succeeded) {
        toast.error(response.message ?? "Unable to send invitation");
        return;
      }

      toast.success(response.message);
      reset();
      setInviteOpen(false);
      await loadOverview();
    } catch (error) {
      console.error("Invitation request failed:", error);
      toast.error("Unable to send invitation");
    } finally {
      setSending(false);
    }
  };

  const openInviteDialog = () => {
    reset({ email: "", roleId: "" });
    setInviteOpen(true);
  };

  return (
    <div className="mx-auto max-w-7xl space-y-6">
      <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h1 className="text-2xl font-semibold tracking-tight text-slate-900">
            User access
          </h1>
          <p className="mt-1 text-sm text-slate-500">
            Manage active users and invitations to your workspace.
          </p>
        </div>
        <Button onClick={openInviteDialog} className="self-start sm:self-auto">
          <Plus className="h-4 w-4" aria-hidden="true" />
          Invite user
        </Button>
      </div>

      <div className="grid gap-4 sm:grid-cols-2">
        <Card>
          <CardContent className="flex items-center gap-4 p-5">
            <div className="rounded-xl bg-blue-50 p-3 text-blue-700">
              <Users className="h-5 w-5" aria-hidden="true" />
            </div>
            <div>
              <p className="text-sm text-muted-foreground">Active users</p>
              <p className="text-2xl font-semibold">
                {loadingOverview || overviewError
                  ? "—"
                  : overview.activeUsers.length}
              </p>
            </div>
          </CardContent>
        </Card>
        <Card>
          <CardContent className="flex items-center gap-4 p-5">
            <div className="rounded-xl bg-amber-50 p-3 text-amber-700">
              <Clock3 className="h-5 w-5" aria-hidden="true" />
            </div>
            <div>
              <p className="text-sm text-muted-foreground">
                Invitations not accepted
              </p>
              <p className="text-2xl font-semibold">
                {loadingOverview || overviewError
                  ? "—"
                  : overview.pendingInvitations.length}
              </p>
            </div>
          </CardContent>
        </Card>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Active users</CardTitle>
          {/* <CardDescription>
            People who have accepted an invitation and can access the workspace.
          </CardDescription> */}
        </CardHeader>
        <CardContent>
          <div className="overflow-x-auto">
            <table className="w-full min-w-[600px] text-left text-sm">
              <thead className="border-b text-xs uppercase tracking-wide text-slate-500">
                <tr>
                  <th scope="col" className="px-3 py-3 font-medium">User</th>
                  <th scope="col" className="px-3 py-3 font-medium">Role</th>
                  <th scope="col" className="px-3 py-3 font-medium">Joined</th>
                </tr>
              </thead>
              <tbody className="divide-y">
                {loadingOverview ? (
                  <tr>
                    <td colSpan={3} className="px-3 py-8 text-center text-slate-500">
                      Loading active users...
                    </td>
                  </tr>
                ) : overviewError ? (
                  <tr>
                    <td colSpan={3} className="px-3 py-8 text-center text-red-600">
                      {overviewError}{" "}
                      <button
                        type="button"
                        onClick={() => void loadOverview()}
                        className="font-medium underline underline-offset-2"
                      >
                        Retry
                      </button>
                    </td>
                  </tr>
                ) : overview.activeUsers.length === 0 ? (
                  <tr>
                    <td colSpan={3} className="px-3 py-8 text-center text-slate-500">
                      No active users found.
                    </td>
                  </tr>
                ) : (
                  overview.activeUsers.map((user) => (
                    <tr key={user.userId}>
                      <td className="px-3 py-4">
                        <div className="font-medium text-slate-900">
                          {user.firstName} {user.lastName}
                        </div>
                        <div className="text-slate-500">{user.email}</div>
                      </td>
                      <td className="px-3 py-4">
                        <div className="flex flex-wrap gap-1.5">
                          {user.roles.length > 0 ? (
                            user.roles.map((role) => (
                              <Badge key={role} variant="secondary">{role}</Badge>
                            ))
                          ) : (
                            <span className="text-slate-500">No role assigned</span>
                          )}
                        </div>
                      </td>
                      <td className="px-3 py-4 text-slate-600">
                        {formatDate(user.createdAt)}
                      </td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Invitations</CardTitle>
          {/* <CardDescription>
            Invitations that have not been accepted yet. Expired invitations are
            marked so they are easy to identify.
          </CardDescription> */}
        </CardHeader>
        <CardContent>
          <div className="overflow-x-auto">
            <table className="w-full min-w-[600px] text-left text-sm">
              <thead className="border-b text-xs uppercase tracking-wide text-slate-500">
                <tr>
                  <th scope="col" className="px-3 py-3 font-medium">Email</th>
                  <th scope="col" className="px-3 py-3 font-medium">Role</th>
                  <th scope="col" className="px-3 py-3 font-medium">Sent</th>
                  <th scope="col" className="px-3 py-3 font-medium">Status</th>
                </tr>
              </thead>
              <tbody className="divide-y">
                {loadingOverview ? (
                  <tr>
                    <td colSpan={4} className="px-3 py-8 text-center text-slate-500">
                      Loading invitations...
                    </td>
                  </tr>
                ) : overviewError ? (
                  <tr>
                    <td colSpan={4} className="px-3 py-8 text-center text-red-600">
                      {overviewError}{" "}
                      <button
                        type="button"
                        onClick={() => void loadOverview()}
                        className="font-medium underline underline-offset-2"
                      >
                        Retry
                      </button>
                    </td>
                  </tr>
                ) : overview.pendingInvitations.length === 0 ? (
                  <tr>
                    <td colSpan={4} className="px-3 py-8 text-center text-slate-500">
                      No unaccepted invitations.
                    </td>
                  </tr>
                ) : (
                  overview.pendingInvitations.map((invitation) => {
                    const isExpired = new Date(invitation.expiresAt) <= new Date();
                    return (
                      <tr key={invitation.userInvitationId}>
                        <td className="px-3 py-4 font-medium text-slate-900">
                          {invitation.email}
                        </td>
                        <td className="px-3 py-4 text-slate-600">
                          {invitation.role}
                        </td>
                        <td className="px-3 py-4 text-slate-600">
                          {formatDate(invitation.createdAt)}
                        </td>
                        <td className="px-3 py-4">
                          <Badge
                            variant={isExpired ? "destructive" : "secondary"}
                            className={
                              isExpired
                                ? ""
                                : "border-transparent bg-emerald-100 text-emerald-800 hover:bg-emerald-100"
                            }
                          >
                            {isExpired ? "Expired" : "Pending"}
                          </Badge>
                        </td>
                      </tr>
                    );
                  })
                )}
              </tbody>
            </table>
          </div>
        </CardContent>
      </Card>

      <Dialog
        open={inviteOpen}
        onOpenChange={(open) => {
          setInviteOpen(open);
          if (!open) reset();
        }}
      >
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Invite a user</DialogTitle>
            <DialogDescription>
              Send an email invitation. The link expires after 7 days.
            </DialogDescription>
          </DialogHeader>
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
                {...register("roleId")}
              >
                <option value="">
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
            <DialogFooter>
              <Button
                type="button"
                variant="outline"
                onClick={() => setInviteOpen(false)}
                disabled={sending}
              >
                Cancel
              </Button>
              <Button
                type="submit"
                disabled={sending || loadingRoles || roles.length === 0}
              >
                <Mail className="h-4 w-4" aria-hidden="true" />
                {sending ? "Sending..." : "Send invitation"}
              </Button>
            </DialogFooter>
          </form>
        </DialogContent>
      </Dialog>
    </div>
  );
};

export default InviteUsers;
