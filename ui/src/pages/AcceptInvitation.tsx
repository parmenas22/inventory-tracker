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
import { invitationService } from "@/services/invitation.service";
import {
  acceptInvitationSchema,
  type AcceptInvitationSchema,
} from "@/validators/invitationSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { useNavigate, useSearchParams } from "react-router-dom";
import { toast } from "sonner";

const AcceptInvitation = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const token = searchParams.get("token") ?? "";
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<AcceptInvitationSchema>({
    resolver: zodResolver(acceptInvitationSchema),
  });

  const onSubmit = async (data: AcceptInvitationSchema) => {
    try {
      const response = await invitationService.accept({
        token,
        firstName: data.firstName,
        lastName: data.lastName,
        password: data.password,
      });
      if (!response.succeeded) {
        toast.error(response.message ?? "Unable to accept invitation");
        return;
      }

      toast.success(response.message);
      navigate("/login");
    } catch (error) {
      console.error("Invitation acceptance failed:", error);
      toast.error("Unable to accept invitation");
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-linear-to-br from-green-50 via-white to-neutral-50 p-4">
      <Card className="w-full max-w-md border border-green-100 shadow-sm">
        <CardHeader className="space-y-2 text-center">
          <CardTitle className="text-2xl font-semibold">Join Inventory Tracker</CardTitle>
          <CardDescription>
            Create your account to accept the administrator&apos;s invitation.
          </CardDescription>
        </CardHeader>
        <CardContent>
          {!token ? (
            <p className="text-center text-sm text-red-600">
              This invitation link is missing its token. Please use the full
              link from your invitation email.
            </p>
          ) : (
            <form className="space-y-5" onSubmit={handleSubmit(onSubmit)}>
              <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
                <div className="space-y-2">
                  <Label htmlFor="firstName">First name</Label>
                  <Input id="firstName" autoComplete="given-name" {...register("firstName")} />
                  {errors.firstName && (
                    <p className="text-sm text-red-600">{errors.firstName.message}</p>
                  )}
                </div>
                <div className="space-y-2">
                  <Label htmlFor="lastName">Last name</Label>
                  <Input id="lastName" autoComplete="family-name" {...register("lastName")} />
                  {errors.lastName && (
                    <p className="text-sm text-red-600">{errors.lastName.message}</p>
                  )}
                </div>
              </div>
              <div className="space-y-2">
                <Label htmlFor="password">Password</Label>
                <Input
                  id="password"
                  type="password"
                  autoComplete="new-password"
                  {...register("password")}
                />
                {errors.password && (
                  <p className="text-sm text-red-600">{errors.password.message}</p>
                )}
              </div>
              <div className="space-y-2">
                <Label htmlFor="confirmPassword">Confirm password</Label>
                <Input
                  id="confirmPassword"
                  type="password"
                  autoComplete="new-password"
                  {...register("confirmPassword")}
                />
                {errors.confirmPassword && (
                  <p className="text-sm text-red-600">
                    {errors.confirmPassword.message}
                  </p>
                )}
              </div>
              <Button type="submit" className="w-full" disabled={isSubmitting}>
                {isSubmitting ? "Creating account..." : "Accept invitation"}
              </Button>
            </form>
          )}
        </CardContent>
      </Card>
    </div>
  );
};

export default AcceptInvitation;
