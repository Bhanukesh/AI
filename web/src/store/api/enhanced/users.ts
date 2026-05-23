import { emptySplitApi } from '../empty-api'

export interface UserItem {
    id: string
    name: string
    email: string
    whatsAppNumber: string
    isNewUser: boolean
    historyBiteIndex: number
    dateJoined: string
    lastSentAt: string | null
}

export interface RegisterUserCommand {
    name: string
    email: string
    whatsAppNumber: string
}

export interface UpdateUserDto {
    isNewUser?: boolean
    historyBiteIndex?: number
    lastSentAt?: string
}

const usersApi = emptySplitApi.injectEndpoints({
    endpoints: (builder) => ({
        getUsers: builder.query<UserItem[], void>({
            query: () => '/users',
            providesTags: ['USER'],
        }),
        registerUser: builder.mutation<string, RegisterUserCommand>({
            query: (body) => ({ url: '/users/register', method: 'POST', body }),
            invalidatesTags: ['USER'],
        }),
        updateUser: builder.mutation<void, { id: string; dto: UpdateUserDto }>({
            query: ({ id, dto }) => ({ url: `/users/${id}`, method: 'PUT', body: dto }),
            invalidatesTags: ['USER'],
        }),
    }),
    overrideExisting: false,
})

export const { useGetUsersQuery, useRegisterUserMutation, useUpdateUserMutation } = usersApi
