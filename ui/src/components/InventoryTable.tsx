import { type Dispatch, type SetStateAction } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "./ui/card";
import {
  Check,
  ChevronDown,
  Edit,
  Package,
  Search,
  Trash2,
} from "lucide-react";
import { Input } from "./ui/input";
import { Button } from "./ui/button";
import { Badge } from "./ui/badge";
import type { Category, Filters, Product } from "@/pages/Dashboard";
import { Popover, PopoverContent, PopoverTrigger } from "./ui/popover";

interface InventoryTableProps {
  products: Product[];
  onEdit: (id: string) => void;
  filters: Filters;
  setFilters: Dispatch<SetStateAction<Filters>>;
  categories: Category[];
}
export const InventoryTable = ({
  products,
  onEdit,
  filters,
  setFilters,
  categories,
}: InventoryTableProps) => {
  const getStockStatus = (quantity: number, minStock: number) => {
    if (quantity === 0)
      return {
        label: "Out of Stock",
        variant: "destructive",
        className:
          "bg-red-100 text-red-800 dark:bg-red-900 dark:text-red-200" as const,
      };
    if (quantity <= minStock)
      return {
        label: "Low Stock",
        variant: "secondary",
        className:
          "bg-amber-100 text-amber-800 dark:bg-amber-900 dark:text-amber-200" as const,
      };
    return {
      label: "In Stock",
      variant: "outline",
      className:
        "bg-green-100 text-green-800 dark:bg-green-900 dark:text-green-200" as const,
    };
  };

  return (
    <Card className="border-border shadow-sm">
      <CardHeader className="border-b border-border bg-muted/30">
        <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
          <CardTitle className="text-xl font-semibold text-foreground">
            Inventory Items
          </CardTitle>

          <Popover>
            <PopoverTrigger asChild>
              <Button
                variant="outline"
                className="w-full sm:w-64 justify-between text-neutral-700 hover:text-neutral-900"
              >
                {filters.category
                  ? categories.find((c) => c.categoryId === filters.category)
                      ?.name || "Filter by Category"
                  : "Filter by Category"}
                <ChevronDown className="ml-2 h-4 w-4 opacity-70" />
              </Button>
            </PopoverTrigger>

            <PopoverContent
              align="start"
              className="w-[var(--radix-popover-trigger-width)] p-3 bg-white border rounded-md shadow-lg"
            >
              <div className="flex flex-col space-y-1">
                {categories.length > 0 ? (
                  categories.map((category) => (
                    <button
                      key={category.categoryId}
                      onClick={() =>
                        setFilters((prev) => ({
                          ...prev,
                          category: category.categoryId,
                        }))
                      }
                      className={`flex items-center justify-between rounded-md px-2 py-1.5 text-sm transition-colors ${
                        filters.category === category.categoryId
                          ? "bg-green-100 text-green-700"
                          : "hover:bg-gray-100 text-gray-700"
                      }`}
                    >
                      {category.name}
                      {filters.category === category.categoryId && (
                        <Check className="h-4 w-4 text-green-600" />
                      )}
                    </button>
                  ))
                ) : (
                  <p className="text-sm text-gray-400 text-center py-2">
                    No categories found
                  </p>
                )}
              </div>

              {/* Clear Filter Button */}
              {categories.length > 0 && (
                <>
                  <div className="border-t my-2"></div>
                  <button
                    onClick={() =>
                      setFilters((prev) => ({ ...prev, category: "" }))
                    }
                    className="w-full text-sm text-red-500 hover:bg-red-50 rounded-md px-2 py-1.5 transition-colors"
                  >
                    Clear Filter
                  </button>
                </>
              )}
            </PopoverContent>
          </Popover>

          <div className="relative w-full sm:w-64">
            <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-muted-foreground" />
            <Input
              placeholder="Search items..."
              value={filters.searchTerm}
              onChange={(e) =>
                setFilters((prev) => ({ ...prev, searchTerm: e.target.value }))
              }
              className="pl-9 bg-background border-border"
            />
          </div>
        </div>
      </CardHeader>
      <CardContent className="p-0">
        {products.length === 0 ? (
          <div className="flex flex-col items-center justify-center py-16 text-center">
            <div className="bg-muted/50 p-4 rounded-xl mb-4">
              <Package className="h-12 w-12 text-muted-foreground" />
            </div>
            <h3 className="text-lg font-medium text-foreground mb-2">
              No items found
            </h3>
          </div>
        ) : (
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead className="bg-muted/20 border-b border-border">
                <tr>
                  <th className="text-left p-4 text-sm font-medium text-muted-foreground">
                    Item
                  </th>
                  <th className="text-left p-4 text-sm font-medium text-muted-foreground">
                    Category
                  </th>
                  <th className="text-left p-4 text-sm font-medium text-muted-foreground">
                    Quantity
                  </th>
                  <th className="text-left p-4 text-sm font-medium text-muted-foreground">
                    Price
                  </th>
                  <th className="text-left p-4 text-sm font-medium text-muted-foreground">
                    Status
                  </th>
                  <th className="text-left p-4 text-sm font-medium text-muted-foreground">
                    Actions
                  </th>
                </tr>
              </thead>
              <tbody>
                {products.map((item) => {
                  const status = getStockStatus(
                    item.quantity,
                    item.minStockAlert
                  );

                  return (
                    <tr
                      key={item.name}
                      className="border-b border-border hover:bg-muted/20 transition-smooth"
                    >
                      <td className="p-4">
                        <span className="font-medium text-foreground">
                          {item.name}
                        </span>
                      </td>
                      <td className="p-4">
                        <span className="text-sm text-muted-foreground">
                          {item.category}
                        </span>
                      </td>
                      <td className="p-4">
                        <span className="text-foreground">{item.quantity}</span>
                      </td>
                      <td className="p-4">
                        <span className="text-foreground">
                          ${item.price.toFixed(2)}
                        </span>
                      </td>
                      <td className="p-4">
                        <Badge className={status.className}>
                          {status.label}
                        </Badge>
                      </td>
                      <td className="p-4">
                        <div className="flex justify-start gap-2">
                          <Button
                            variant="ghost"
                            size="sm"
                            onClick={() => onEdit(item.productId)}
                            className="hover:bg-accent hover:text-accent-foreground transition-smooth"
                          >
                            <Edit className="h-4 w-4" />
                          </Button>
                          <Button
                            variant="ghost"
                            size="sm"
                            // onClick={() => onDelete(item.id)}
                            className="hover:bg-destructive/10 hover:text-destructive transition-smooth"
                          >
                            <Trash2 className="h-4 w-4" />
                          </Button>
                        </div>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        )}
      </CardContent>
    </Card>
  );
};
