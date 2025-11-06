import { useEffect } from "react";
import { Button } from "./ui/button";
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "./ui/dialog";
import { Label } from "./ui/label";
import { Input } from "./ui/input";
import type { Category, Product } from "@/pages/Dashboard";
import { Popover, PopoverContent, PopoverTrigger } from "./ui/popover";
import { Check, ChevronDown } from "lucide-react";
import { useAuth } from "@/context/AuthContext";
import { ProductSchema } from "@/validators/productSchema";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

interface ItemDialogProps {
  open: boolean;
  onClose: () => void;
  onSave: (item: Product) => void;
  product: Product | null;
  categories: Category[];
}

export const ItemDialog = ({
  open,
  onClose,
  onSave,
  product,
  categories,
}: ItemDialogProps) => {
  const { user } = useAuth();

  const {
    register,
    handleSubmit,
    setValue,
    watch,
    reset,
    formState: { errors },
  } = useForm<ProductSchema>({
    resolver: zodResolver(ProductSchema),
    defaultValues: {
      name: "",
      categoryId: "",
      quantity: 0,
      price: 0,
      minStockAlert: 10,
    },
  });

  const selectedCategoryId = watch("categoryId");

  useEffect(() => {
    if (product) {
      const selectedCategory = categories.find(
        (c) => c.name == product.category
      );
      reset({ ...product, categoryId: selectedCategory?.categoryId || "" });
    } else {
      reset({
        name: "",
        categoryId: "",
        quantity: 0,
        price: 0,
        minStockAlert: 10,
      });
    }
  }, [product, open, reset, user]);

  const onSubmit = (data: ProductSchema) => {
    onSave(data as Product);
    onClose();
  };

  return (
    <Dialog open={open} onOpenChange={onClose}>
      <DialogContent className="sm:max-w-md bg-card border-border">
        <DialogHeader>
          <DialogTitle className="text-foreground">
            {product ? "Edit Item" : "Add New Item"}
          </DialogTitle>
        </DialogHeader>
        <form onSubmit={handleSubmit(onSubmit)}>
          <div className="space-y-4 py-4">
            <div className="space-y-2">
              <Label htmlFor="name" className="text-foreground">
                Item Name
              </Label>
              <Input
                id="name"
                placeholder="e.g., Laptop"
                {...register("name")}
                className="bg-background border-border"
              />
              {errors.name && (
                <p className="text-red-500 text-sm">{errors.name.message}</p>
              )}
            </div>

            {/* Category Selection */}
            <div className="space-y-2">
              <Label htmlFor="category">Category</Label>
              <Popover>
                <PopoverTrigger asChild>
                  <Button
                    type="button"
                    variant="outline"
                    className="w-full justify-between"
                  >
                    {selectedCategoryId
                      ? categories.find(
                          (c) => c.categoryId === selectedCategoryId
                        )?.name
                      : "Select Category..."}
                    <ChevronDown className="ml-2 h-4 w-4 opacity-60" />
                  </Button>
                </PopoverTrigger>
                <PopoverContent
                  className="w-full p-2 space-y-1 max-h-64 overflow-y-auto"
                  align="start"
                >
                  {categories.length > 0 ? (
                    categories.map((category) => (
                      <Button
                        key={category.categoryId}
                        variant="ghost"
                        className="w-full justify-start text-left"
                        onClick={() =>
                          setValue("categoryId", category.categoryId)
                        }
                      >
                        {selectedCategoryId === category.categoryId && (
                          <Check className="mr-2 h-4 w-4" />
                        )}
                        {category.name}
                      </Button>
                    ))
                  ) : (
                    <p className="text-sm text-muted-foreground px-2">
                      No categories found
                    </p>
                  )}
                </PopoverContent>
              </Popover>
            </div>

            <div className="grid grid-cols-2 gap-4">
              <div className="space-y-2">
                <Label htmlFor="quantity" className="text-foreground">
                  Quantity
                </Label>
                <Input
                  id="quantity"
                  type="number"
                  min="0"
                  {...register("quantity", { valueAsNumber: true })}
                  className="bg-background border-border"
                />
                {errors.quantity && (
                  <p className="text-red-500 text-sm">
                    {errors.quantity.message}
                  </p>
                )}
              </div>
              <div className="space-y-2">
                <Label htmlFor="price" className="text-foreground">
                  Price ($)
                </Label>
                <Input
                  id="price"
                  type="number"
                  min="0"
                  {...register("price", { valueAsNumber: true })}
                  className="bg-background border-border"
                />
                {errors.price && (
                  <p className="text-red-500 text-sm">{errors.price.message}</p>
                )}
              </div>
            </div>
            <div className="space-y-2">
              <Label htmlFor="minStock" className="text-foreground">
                Minimum Stock Alert
              </Label>
              <Input
                id="minStock"
                type="number"
                min="0"
                {...register("minStockAlert", { valueAsNumber: true })}
                className="bg-background border-border"
              />
              {errors.minStockAlert && (
                <p className="text-red-500 text-sm">
                  {errors.minStockAlert.message}
                </p>
              )}
            </div>
          </div>
          <DialogFooter className="gap-2">
            <Button
              type="button"
              variant="outline"
              onClick={onClose}
              className="border-border"
            >
              Cancel
            </Button>
            <Button
              type="submit"
              className="bg-green-500 hover:bg-green-600 text-white"
            >
              {product ? "Update" : "Add"} Item
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
};
