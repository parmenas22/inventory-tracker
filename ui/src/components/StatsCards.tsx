import { AlertTriangle, Archive, Package, TrendingUp } from "lucide-react";
import { Card, CardContent } from "./ui/card";

interface StatsCardsProps {
  totalItems: number;
  lowStockItems: number;
  totalValue: number;
  categories: number;
}
export const StatsCards = ({
  totalItems,
  lowStockItems,
  totalValue,
  categories,
}: StatsCardsProps) => {
  const stats = [
    {
      title: "Total Items",
      value: totalItems,
      icon: Package,
      bgColor: "bg-primary/10",
      iconColor: "text-primary",
    },
    {
      title: "Low Stock",
      value: lowStockItems,
      icon: AlertTriangle,
      bgColor: "bg-warning/10",
      iconColor: "text-warning",
    },
    {
      title: "Total Value",
      value: `$${totalValue.toLocaleString()}`,
      icon: TrendingUp,
      bgColor: "bg-success/10",
      iconColor: "text-success",
    },
    {
      title: "Categories",
      value: categories,
      icon: Archive,
      bgColor: "bg-accent",
      iconColor: "text-accent-foreground",
    },
  ];

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
      {stats.map((stat) => {
        const Icon = stat.icon;
        return (
          <Card
            key={stat.title}
            className="border-border shadow-sm hover:shadow-md transition-smooth"
          >
            <CardContent className="p-6">
              <div className="flex items-center justify-between">
                <div>
                  <p className="text-sm text-muted-foreground font-medium">
                    {stat.title}
                  </p>
                  <p className="text-2xl font-semibold text-foreground mt-2">
                    {stat.value}
                  </p>
                </div>
                <div className={`${stat.bgColor} p-3 rounded-xl`}>
                  <Icon className={`h-5 w-5 ${stat.iconColor}`} />
                </div>
              </div>
            </CardContent>
          </Card>
        );
      })}
    </div>
  );
};
