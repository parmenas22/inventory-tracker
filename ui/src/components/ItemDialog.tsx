import React, { useEffect, useState } from "react";
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
  product?: Product;
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
  const [formData, setFormData] = useState<Product>({
    name: "",
    category: "",
    quantity: 0,
    price: 0,
    minStockAlert: 10,
    createdBy: "",
    productId: "",
  });

  useEffect(() => {
    if (product) {
      setFormData({
        name: product.name,
        category: product.category,
        quantity: product.quantity,
        price: product.price,
        minStockAlert: product.minStockAlert,
        createdBy: user?.UserId,
        productId: product.productId,
      });
    } else {
      setFormData({
        name: "",
        category: "",
        quantity: 0,
        price: 0,
        minStockAlert: 10,
        createdBy: user?.UserId,
        productId: "",
      });
    }
  }, [product, open]);

  const onSubmit = (data: ProductSchema) => {
    onSave(formData);
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
        <form>
          <div className="space-y-4 py-4">
            <div className="space-y-2">
              <Label htmlFor="name" className="text-foreground">
                Item Name
              </Label>
              <Input
                id="name"
                placeholder="e.g., Laptop"
                value={formData.name}
                onChange={(e) =>
                  setFormData({ ...formData, name: e.target.value })
                }
                required
                className="bg-background border-border"
              />
            </div>

            {/* Category Selection */}
            <div className="space-y-2">
              <Label htmlFor="category">Category</Label>
              <Popover>
                <PopoverTrigger asChild>
                  <Button
                    variant="outline"
                    className={"w-full justify-between"}
                  >
                    {formData.category
                      ? categories.find((c) => c.name === formData.category)
                          ?.name
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
                          setFormData((prev) => ({
                            ...prev,
                            category: category.name,
                          }))
                        }
                      >
                        {formData.category === category.name && (
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
                  value={formData.quantity}
                  onChange={(e) =>
                    setFormData({
                      ...formData,
                      quantity: parseInt(e.target.value) || 0,
                    })
                  }
                  required
                  className="bg-background border-border"
                />
              </div>
              <div className="space-y-2">
                <Label htmlFor="price" className="text-foreground">
                  Price ($)
                </Label>
                <Input
                  id="price"
                  type="number"
                  min="0"
                  value={Number(formData.price)}
                  onChange={(e) =>
                    setFormData({
                      ...formData,
                      price: Number.parseFloat(e.target.value) || 0,
                    })
                  }
                  required
                  className="bg-background border-border"
                />
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
                value={formData.minStockAlert}
                onChange={(e) =>
                  setFormData({
                    ...formData,
                    minStockAlert: parseInt(e.target.value) || 0,
                  })
                }
                required
                className="bg-background border-border"
              />
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
