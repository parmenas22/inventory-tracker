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
import { forgotPasswordSchema, type ForgotPasswordSchema } from "@/validators/forgotPasswordSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { toast } from "sonner";
import { authService } from "@/services/auth.service";
import { useNavigate } from "react-router-dom";

const ForgotPassword = () => {
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<ForgotPasswordSchema>({
    resolver: zodResolver(forgotPasswordSchema),
  });

  const onSubmit = async (data: ForgotPasswordSchema) => {
    const res = await authService.forgotPassword(data.email);
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
          <CardTitle className="text-2xl font-semibold">Forgot Password</CardTitle>
          <CardDescription className="text-neutral-600">
            Enter your email and we will send instructions to reset your password.
          </CardDescription>
        </CardHeader>

        <CardContent className="space-y-4">
          <form className="space-y-8" onSubmit={handleSubmit(onSubmit)}>
            <div className="space-y-4">
              <Label htmlFor="email">Email</Label>
              <Input
                id="email"
                type="email"
                placeholder="name@example.com"
                className="border-green-200 focus:border-green-400"
                {...register("email")}
              />
              {errors.email && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.email.message}
                </p>
              )}
            </div>

            <Button
              type="submit"
              className="w-full bg-green-500 hover:bg-green-600 text-white"
            >
              Send Reset Link
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

export default ForgotPassword;
