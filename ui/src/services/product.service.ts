import apiClient from "@/global/apiClient";
import type { ApiResponse } from "./auth.service";
import type { Filters } from "@/pages/Dashboard";

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
};
