import { z } from "zod";

export const ProductSchema = z.object({
  name: z.string().nonempty({ message: "Item name is required" }),
  category: z.string().nonempty(),
  quantity: z.number(),
  price: z.number(),
  minStockAlert: z.number(),
});

export type ProductSchema = z.infer<typeof ProductSchema>;
