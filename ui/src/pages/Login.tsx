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
import { loginSchema, type LoginSchema } from "@/validators/loginSchema";
import { Package } from "lucide-react";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { toast } from "sonner";
import { useNavigate } from "react-router-dom";
import { useAuth } from "@/context/AuthContext";

const Login = () => {
  const navigate = useNavigate();
  const { login } = useAuth();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginSchema>({
    resolver: zodResolver(loginSchema),
  });

  const onSubmit = async (data: LoginSchema): Promise<void> => {
    const res = await login(data);
    if (res?.succeeded) {
      setTimeout(() => {
        navigate("/");
      }, 1000);
      toast.success(res.message);
    }
    toast.error(res?.message ?? res?.error);
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-linear-to-br from-green-50 via-white to-neutral-50 p-4">
      <Card className="w-full max-w-md shadow-sm border border-green-100">
        <CardHeader className="space-y-2 text-center">
          <div className="flex justify-center mb-4">
            <div className="bg-green-100 p-3 rounded-full">
              <Package className="h-8 w-8" />
            </div>
          </div>
          <CardTitle className="text-2xl font-semibold">
            "Welcome Back"
          </CardTitle>
          <CardDescription className="text-neutral-600">
            "Sign in to manage your inventory"
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

            <div className="space-y-2">
              <Label htmlFor="password">Password</Label>
              <Input
                id="password"
                type="password"
                placeholder="••••••••"
                className="border-green-200 focus:border-green-400"
                {...register("password")}
              />
              {errors.password && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.password.message}
                </p>
              )}
            </div>

            <Button
              type="submit"
              className="w-full bg-green-500 hover:bg-green-600 text-white"
            >
              "Sign In"
            </Button>
          </form>

          <div className="text-center text-sm">
            <button
              type="button"
              className="text-green-600 hover:text-green-700 font-medium transition-colors"
            >
              "Don't have an account? Sign up"
            </button>
          </div>
        </CardContent>
      </Card>
    </div>
  );
};

export default Login;
