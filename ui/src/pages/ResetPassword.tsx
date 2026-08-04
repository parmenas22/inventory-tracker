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
import { resetPasswordSchema, type ResetPasswordSchema } from "@/validators/resetPasswordSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { toast } from "sonner";
import { authService } from "@/services/auth.service";
import { useNavigate, useSearchParams } from "react-router-dom";

const ResetPassword = () => {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const resetToken = searchParams.get("token") ?? "";

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<ResetPasswordSchema>({
    resolver: zodResolver(resetPasswordSchema),
  });

  const onSubmit = async (data: ResetPasswordSchema) => {
    if (!resetToken) {
      toast.error("Reset token is missing.");
      return;
    }

    const res = await authService.resetPassword(resetToken, data.newPassword);
    if (res?.succeeded) {
      toast.success(res.message);
      return;
    }

    toast.error(res?.message ?? "Something went wrong");
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-linear-to-br from-green-50 via-white to-neutral-50 p-4">
      <Card className="w-full max-w-md shadow-sm border border-green-100">
        <CardHeader className="space-y-2 text-center">
          <CardTitle className="text-2xl font-semibold">Reset Password</CardTitle>
          <CardDescription className="text-neutral-600">
            Enter a new password for your account.
          </CardDescription>
        </CardHeader>

        <CardContent className="space-y-4">
          <form className="space-y-8" onSubmit={handleSubmit(onSubmit)}>
            <div className="space-y-2">
              <Label htmlFor="newPassword">New Password</Label>
              <Input
                id="newPassword"
                type="password"
                placeholder="••••••••"
                className="border-green-200 focus:border-green-400"
                {...register("newPassword")}
              />
              {errors.newPassword && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.newPassword.message}
                </p>
              )}
            </div>

            <div className="space-y-2">
              <Label htmlFor="confirmPassword">Confirm Password</Label>
              <Input
                id="confirmPassword"
                type="password"
                placeholder="••••••••"
                className="border-green-200 focus:border-green-400"
                {...register("confirmPassword")}
              />
              {errors.confirmPassword && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.confirmPassword.message}
                </p>
              )}
            </div>

            <Button
              type="submit"
              className="w-full bg-green-500 hover:bg-green-600 text-white"
            >
              Reset Password
            </Button>
          </form>

          <div className="text-center text-sm mt-4">
            <button
              type="button"
              onClick={() => navigate("/login")}
              className="text-green-600 hover:text-green-700 font-medium transition-colors"
            >
              Back to login
            </button>
          </div>
        </CardContent>
      </Card>
    </div>
  );
};

export default ResetPassword;
