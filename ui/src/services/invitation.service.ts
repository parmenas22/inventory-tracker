import apiClient from "@/global/apiClient";
import type { ApiResponse } from "@/services/auth.service";

export interface InvitationRole {
  roleId: string;
  name: string;
}

export const invitationService = {
  async getRoles(): Promise<ApiResponse<InvitationRole[]>> {
    const response = await apiClient.get<ApiResponse<InvitationRole[]>>(
      "/invitations/roles"
    );
    return response.data;
  },

  async invite(email: string, roleId: string): Promise<ApiResponse> {
    const response = await apiClient.post<ApiResponse>("/invitations", {
      email,
      roleId,
    });
    return response.data;
  },

  async accept(data: {
    token: string;
    firstName: string;
    lastName: string;
    password: string;
  }): Promise<ApiResponse> {
    const response = await apiClient.post<ApiResponse>(
      "/invitations/accept",
      data
    );
    return response.data;
  },
};
