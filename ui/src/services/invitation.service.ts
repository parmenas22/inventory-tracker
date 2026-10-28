import apiClient from "@/global/apiClient";
import type { ApiResponse } from "@/services/auth.service";

export interface InvitationRole {
  roleId: string;
  name: string;
}

export interface ActiveUser {
  userId: string;
  firstName: string;
  lastName: string;
  email: string;
  roles: string[];
  createdAt: string;
}

export interface PendingInvitation {
  userInvitationId: string;
  email: string;
  role: string;
  createdAt: string;
  expiresAt: string;
}

export interface UserAccessOverview {
  activeUsers: ActiveUser[];
  pendingInvitations: PendingInvitation[];
}

export const invitationService = {
  async getAdminOverview(): Promise<ApiResponse<UserAccessOverview>> {
    const response = await apiClient.get<ApiResponse<UserAccessOverview>>(
      "/invitations/admin/overview"
    );
    return response.data;
  },

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
