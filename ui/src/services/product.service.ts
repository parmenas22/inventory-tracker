import apiClient from "@/global/apiClient";
import type { ApiResponse } from "./auth.service";
import type { Filters } from "@/pages/Dashboard";
import type { ProductSchema } from "@/validators/productSchema";

export const ProductService = {
  async getProducts(filters: Filters): Promise<ApiResponse> {
    //build query filters
    const params = new URLSearchParams();
    if (filters.searchTerm) {
      params.append("searchTerm", filters.searchTerm.trim());
    }
    if (filters.category) {
      params.append("categoryId", filters.category.trim());
    }
    if (filters.lowStockOnly) {
      params.append("lowStockOnly", "true");
    }

    const query = params.toString() ? `${params.toString()}` : "";
    const res = await apiClient.get<ApiResponse>(`/products?${query}`);
    return res.data;
  },

  async getAllCategories(): Promise<ApiResponse> {
    const res = await apiClient.get<ApiResponse>("/products/categories");
    return res.data;
  },

  async getProductById(id: string): Promise<ApiResponse> {
    const res = await apiClient.get<ApiResponse>(`/products/${id}`);
    return res.data;
  },

  async editProduct(id: string, data: ProductSchema): Promise<ApiResponse> {
    const res = await apiClient.put<ApiResponse>(`/products/${id}`, data);
    return res.data;
  },

  async addProduct(data: ProductSchema): Promise<ApiResponse> {
    const res = await apiClient.post<ApiResponse>(`/products`, data);
    return res.data;
  },

  async getDashboardData(): Promise<ApiResponse> {
    const res = await apiClient.get<ApiResponse>("/products/dashboard");
    return res.data;
  },
};
