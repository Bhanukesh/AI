'use client'

import { useState } from 'react'
import { useGetUsersQuery, useRegisterUserMutation } from '@/store/api/enhanced/users'
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'

function StatusBadge({ value, trueLabel = 'Yes', falseLabel = 'No' }: { value: boolean; trueLabel?: string; falseLabel?: string }) {
    return (
        <span className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${
            value ? 'bg-amber-100 text-amber-800' : 'bg-green-100 text-green-800'
        }`}>
            {value ? trueLabel : falseLabel}
        </span>
    )
}

function formatDate(iso: string | null) {
    if (!iso) return '—'
    return new Date(iso).toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric' })
}

export default function UsersPage() {
    const { data: users, isLoading, isError } = useGetUsersQuery()
    const [registerUser, { isLoading: isRegistering }] = useRegisterUserMutation()

    const [showForm, setShowForm] = useState(false)
    const [name, setName] = useState('')
    const [email, setEmail] = useState('')
    const [whatsApp, setWhatsApp] = useState('')
    const [formError, setFormError] = useState('')

    const handleRegister = async () => {
        setFormError('')
        if (!name.trim() || !email.trim() || !whatsApp.trim()) {
            setFormError('All fields are required.')
            return
        }
        if (!whatsApp.startsWith('+')) {
            setFormError('WhatsApp number must be in E.164 format, e.g. +1234567890')
            return
        }
        try {
            await registerUser({ name, email, whatsAppNumber: whatsApp }).unwrap()
            setName(''); setEmail(''); setWhatsApp(''); setShowForm(false)
        } catch {
            setFormError('Registration failed. Email may already exist.')
        }
    }

    if (isLoading) return <p className="text-gray-500">Loading users…</p>
    if (isError) return <p className="text-red-500">Failed to load users.</p>

    return (
        <div>
            <div className="flex items-center justify-between mb-6">
                <div>
                    <h1 className="text-2xl font-bold text-gray-900">Users</h1>
                    <p className="text-sm text-gray-500 mt-1">{users?.length ?? 0} subscribers</p>
                </div>
                <Button onClick={() => setShowForm(!showForm)}>
                    {showForm ? 'Cancel' : '+ Register User'}
                </Button>
            </div>

            {showForm && (
                <div className="bg-white border border-gray-200 rounded-lg p-5 mb-6 max-w-md">
                    <h2 className="font-semibold text-gray-800 mb-4">New Subscriber</h2>
                    <div className="space-y-3">
                        <Input placeholder="Full name" value={name} onChange={e => setName(e.target.value)} />
                        <Input placeholder="Email address" type="email" value={email} onChange={e => setEmail(e.target.value)} />
                        <Input placeholder="WhatsApp (+1234567890)" value={whatsApp} onChange={e => setWhatsApp(e.target.value)} />
                        {formError && <p className="text-sm text-red-600">{formError}</p>}
                        <Button onClick={handleRegister} disabled={isRegistering} className="w-full">
                            {isRegistering ? 'Registering…' : 'Register'}
                        </Button>
                    </div>
                </div>
            )}

            <div className="bg-white border border-gray-200 rounded-lg overflow-hidden">
                <Table>
                    <TableHeader>
                        <TableRow>
                            <TableHead>Name</TableHead>
                            <TableHead>Email</TableHead>
                            <TableHead>WhatsApp</TableHead>
                            <TableHead>New User</TableHead>
                            <TableHead>History #</TableHead>
                            <TableHead>Joined</TableHead>
                            <TableHead>Last Sent</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {users?.length === 0 && (
                            <TableRow>
                                <TableCell colSpan={7} className="text-center text-gray-400 py-8">
                                    No users yet. Register the first subscriber above.
                                </TableCell>
                            </TableRow>
                        )}
                        {users?.map(user => (
                            <TableRow key={user.id}>
                                <TableCell className="font-medium">{user.name}</TableCell>
                                <TableCell className="text-gray-600">{user.email}</TableCell>
                                <TableCell className="text-gray-600 font-mono text-sm">{user.whatsAppNumber}</TableCell>
                                <TableCell><StatusBadge value={user.isNewUser} trueLabel="New" falseLabel="Active" /></TableCell>
                                <TableCell className="text-center">{user.historyBiteIndex}</TableCell>
                                <TableCell className="text-gray-600 text-sm">{formatDate(user.dateJoined)}</TableCell>
                                <TableCell className="text-gray-600 text-sm">{formatDate(user.lastSentAt)}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </div>
        </div>
    )
}
