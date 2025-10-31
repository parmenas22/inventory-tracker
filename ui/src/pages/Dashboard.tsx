import { InventoryTable } from "@/components/InventoryTable";
import { ItemDialog } from "@/components/ItemDialog";
import { StatsCards } from "@/components/StatsCards";
import { Button } from "@/components/ui/button";
import { ProductService } from "@/services/product.service";
import { Plus } from "lucide-react";
import { useEffect, useState } from "react";

export interface Product {
  name: string;
  category: string;
  quantity: number;
  price: number;
  minStockAlert: number;
  createdBy: string;
}

export interface Category {
  name: string;
  categoryId: string;
}

export interface Filters {
  searchTerm: string;
  category: string;
  lowStockOnly: boolean;
}

const Dashboard = () => {
  const [dialogOpen, setDialogOpen] = useState(false);
  const [products, setProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [filters, setFilters] = useState<Filters>({
    searchTerm: "",
    category: "",
    lowStockOnly: false,
  });

  useEffect(() => {
    fetchCategories();
  }, []);

  useEffect(() => {
    fetchProducts();
  }, [filters]);

  const fetchProducts = async () => {
    const res = await ProductService.getProducts(filters);
    if (res.succeeded && res.value) {
      setProducts(res.value);
    }
  };

  const fetchCategories = async () => {
    const res = await ProductService.getAllCategories();
    if (res.succeeded && res.value) {
      setCategories(res.value);
    }
  };

  return (
    <div className="min-h-screen bg-background">
      <div className="flex justify-end px-4 py-6 ">
        <Button
          className="bg-green-500 hover:bg-green-600 text-white"
          onClick={() => setDialogOpen(true)}
        >
          <Plus className="h-4 w-4 mr-2" />
          Add Item
        </Button>
      </div>
      {/* Main content */}
      <main className="container mx-auto px-6 py-8 space-y-8">
        <StatsCards
          totalItems={5}
          lowStockItems={2}
          totalValue={3000}
          categories={3}
        />

        <InventoryTable
          products={products}
          categories={categories}
          onEdit={() => {}}
          onDelete={() => {}}
          filters={filters}
          setFilters={setFilters}
        />
      </main>

      {/* Add/Edit dialog (UI only, closed by default) */}
      <ItemDialog
        open={dialogOpen}
        onClose={() => setDialogOpen(false)}
        onSave={() => setDialogOpen(false)}
      />
    </div>
  );
};

export default Dashboard;
