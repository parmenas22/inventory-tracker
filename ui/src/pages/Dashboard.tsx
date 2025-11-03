import { InventoryTable } from "@/components/InventoryTable";
import { ItemDialog } from "@/components/ItemDialog";
import { StatsCards } from "@/components/StatsCards";
import { Button } from "@/components/ui/button";
import { ProductService } from "@/services/product.service";
import type { ProductSchema } from "@/validators/productSchema";
import { Plus } from "lucide-react";
import { useEffect, useState } from "react";
import { toast } from "sonner";

export interface Product {
  productId: string;
  name: string;
  category: string;
  categoryId: string;
  quantity: number;
  price: number;
  minStockAlert: number;
  createdBy?: string;
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
  const [edit, setEdit] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [selectedProductId, setSelectedProductId] = useState<string>("");
  const [products, setProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [filters, setFilters] = useState<Filters>({
    searchTerm: "",
    category: "",
    lowStockOnly: false,
  });
  const [totalItems, setTotalItems] = useState(0);
  const [totalCategories, setTotalCategories] = useState(0);
  const [lowStockItems, setLowStockItems] = useState(0);
  const [totalValue, setTotalValue] = useState(0);

  useEffect(() => {
    fetchCategories();
    fetchProducts();
    fetchDashboardData();
  }, []);

  useEffect(() => {
    fetchProducts();
  }, [filters]);

  useEffect(() => {
    fetchProductDetails();
  }, [selectedProductId]);

  const fetchDashboardData = async () => {
    const res = await ProductService.getDashboardData();
    if (res.succeeded && res.value) {
      setTotalItems(res.value.totalProducts);
      setTotalValue(res.value.totalValue);
      setTotalCategories(res.value.totalCategories);
      setLowStockItems(res.value.lowStockCount);
    }
  };

  const fetchProductDetails = async () => {
    const res = await ProductService.getProductById(selectedProductId);
    if (res.succeeded && res.value) {
      setSelectedProduct(res.value);
    }
  };
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
      setTotalCategories(res.value.count);
    }
  };

  const handleEdit = (id: string) => {
    setSelectedProductId(id);
    setDialogOpen(true);
    setEdit(true);
  };

  const handleDialogClose = () => {
    // setDialogOpen(false);
  };

  const handleSaveProduct = async (product: Product) => {
    console.log("Product", product);
    let res;
    if (edit && product.productId) {
      res = await ProductService.editProduct(product.productId, product);
    } else {
      res = await ProductService.addProduct(product);
    }
    if (res.succeeded) {
      fetchProducts();
      fetchDashboardData();
      setSelectedProduct(null);
      setEdit(false);
    } else if (res.error) {
      toast.warning(res.error.message);
    }
    setDialogOpen(false);
  };

  const handleAddItem = () => {
    setSelectedProduct(null);
    setDialogOpen(true);
    setEdit(false);
  };

  return (
    <div className="min-h-screen bg-background">
      <div className="flex justify-end px-4 py-6 ">
        <Button
          className="bg-green-500 hover:bg-green-600 text-white"
          onClick={handleAddItem}
        >
          <Plus className="h-4 w-4 mr-2" />
          Add Item
        </Button>
      </div>
      {/* Main content */}
      <main className="container mx-auto px-6 py-8 space-y-8">
        <StatsCards
          totalItems={totalItems}
          lowStockItems={lowStockItems}
          totalValue={totalValue}
          categories={totalCategories}
        />

        <InventoryTable
          products={products}
          categories={categories}
          onEdit={handleEdit}
          onDelete={() => {}}
          filters={filters}
          setFilters={setFilters}
        />
      </main>

      {/* Add/Edit dialog (UI only, closed by default) */}
      <ItemDialog
        open={dialogOpen}
        onClose={handleDialogClose}
        onSave={handleSaveProduct}
        categories={categories}
        product={selectedProduct}
      />
    </div>
  );
};

export default Dashboard;
