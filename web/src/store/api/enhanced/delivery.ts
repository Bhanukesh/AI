import { emptySplitApi } from '../empty-api'

export interface DeliveryLogItem {
    id: string
    userId: string
    userEmail: string
    date: string
    channel: string
    status: string
    briefSummary: string
    createdAt: string
}

export interface LogDeliveryCommand {
    userId: string
    channel: string
    status: string
    briefSummary: string
}

const deliveryApi = emptySplitApi.injectEndpoints({
    endpoints: (builder) => ({
        getDeliveryLogs: builder.query<DeliveryLogItem[], { userId?: string; latest?: boolean } | void>({
            query: (params) => {
                const search = new URLSearchParams()
                if (params?.userId) search.set('userId', params.userId)
                if (params?.latest) search.set('latest', 'true')
                const qs = search.toString()
                return `/delivery/logs${qs ? `?${qs}` : ''}`
            },
            providesTags: ['LOG'],
        }),
        logDelivery: builder.mutation<void, LogDeliveryCommand>({
            query: (body) => ({ url: '/delivery/log', method: 'POST', body }),
            invalidatesTags: ['LOG'],
        }),
    }),
    overrideExisting: false,
})

export const { useGetDeliveryLogsQuery, useLogDeliveryMutation } = deliveryApi
